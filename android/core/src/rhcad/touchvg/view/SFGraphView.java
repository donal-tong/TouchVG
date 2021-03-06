// Copyright (c) 2014, https://github.com/rhcad/touchvg

package rhcad.touchvg.view;

import rhcad.touchvg.core.GiCoreView;
import rhcad.touchvg.core.GiView;
import rhcad.touchvg.view.internal.BaseViewAdapter;
import rhcad.touchvg.view.internal.ContextAction;
import rhcad.touchvg.view.internal.GestureListener;
import rhcad.touchvg.view.internal.ImageCache;
import rhcad.touchvg.view.internal.ResourceUtil;
import rhcad.touchvg.view.internal.ViewUtil;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.PixelFormat;
import android.graphics.PorterDuff.Mode;
import android.graphics.drawable.Drawable;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;

/**
 * \ingroup GROUP_ANDROID
 * Graphics view with media overlay surface placed behind its window.
 * It uses a surface view placed on top of its window to draw dynamic shapes.
 */
public class SFGraphView extends SurfaceView implements GraphView {
    protected static final String TAG = "touchvg";
    protected GiCoreView mCoreView;
    protected SFViewAdapter mViewAdapter;
    protected ImageCache mImageCache;

    protected RenderRunnable mRender;
    protected SurfaceView mDynDrawView;
    protected DynRenderRunnable mDynDrawRender;

    protected int mBkColor = Color.WHITE;
    protected Drawable mBackground;
    protected GestureDetector mGestureDetector;
    protected GestureListener mGestureListener;
    protected boolean mGestureEnable = true;

    static {
        System.loadLibrary("touchvg");
    }

    public SFGraphView(Context context) {
        super(context);
        createAdapter(context);
        mCoreView = new GiCoreView();
        mCoreView.createView(mViewAdapter);
        initView(context);
        if (ViewUtil.activeView == null) {
            ViewUtil.activeView = this;
        }
    }

    public SFGraphView(Context context, GraphView mainView) {
        super(context);
        createAdapter(context);
        mCoreView = new GiCoreView(mainView.coreView());
        mCoreView.createMagnifierView(mViewAdapter, mainView.viewAdapter());
        initView(context);
    }

    protected void createAdapter(Context context) {
        mImageCache = new ImageCache();
        mViewAdapter = new SFViewAdapter();
    }

    protected void initView(Context context) {
        getHolder().addCallback(new SurfaceCallback());
        setZOrderMediaOverlay(true); // see setBackgroundColor

        mGestureListener = new GestureListener(mCoreView, mViewAdapter);
        mGestureDetector = new GestureDetector(context, mGestureListener);
        ResourceUtil.setContextImages(context);

        final DisplayMetrics dm = context.getApplicationContext()
                .getResources().getDisplayMetrics();
        GiCoreView.setScreenDpi(dm.densityDpi, dm.density);

        this.setOnTouchListener(new OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                if (event.getActionMasked() == MotionEvent.ACTION_DOWN) {
                    activateView();
                }
                return mGestureEnable && (mGestureListener.onTouch(v, event)
                                          || mGestureDetector.onTouchEvent(event));
            }
        });
    }

    protected void activateView() {
        mViewAdapter.removeContextButtons();
        if (ViewUtil.activeView != this) {
            ViewUtil.activeView = this;
        }
    }

    @Override
    protected void onDetachedFromWindow() {
        if (ViewUtil.activeView == this) {
            ViewUtil.activeView = null;
        }
        mDynDrawView = null;

        super.onDetachedFromWindow();

        if (mImageCache != null) {
            synchronized (mImageCache) {
            }
            mImageCache.clear();
            mImageCache = null;
        }
        mGestureListener.release();
        mCoreView.destoryView(mViewAdapter);
        mViewAdapter.delete();
        mCoreView.delete();
    }

    private int drawShapes(CanvasAdapter canvasAdapter) {
        int doc, gs, n;

        synchronized (mCoreView) {
            doc = mCoreView.acquireFrontDoc();
            gs = mCoreView.acquireGraphics(mViewAdapter);
        }
        try {
            n = mCoreView.drawAll(doc, gs, canvasAdapter);
        } finally {
            GiCoreView.releaseDoc(doc);
            mCoreView.releaseGraphics(gs);
        }

        return n;
    }

    protected class RenderRunnable implements Runnable {
        private CanvasAdapter mCanvasAdapter = new CanvasAdapter(null, mImageCache);

        public void requestRender() {
            synchronized (this) {
                this.notify();
            }
        }

        public void stop() {
            if (mCanvasAdapter != null) {
                mViewAdapter.stop();
                requestRender();
                synchronized (mCanvasAdapter) {
                    try {
                        mCanvasAdapter.wait(1000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
                mCanvasAdapter.delete();
                mCanvasAdapter = null;
            }
        }

        @Override
        public void run() {
            while (!mViewAdapter.isStopping()) {
                synchronized (this) {
                    try {
                        this.wait();
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
                if (!mViewAdapter.isStopping())
                    render();
            }
            if (mCanvasAdapter != null) {
                synchronized (mCanvasAdapter) {
                    mCanvasAdapter.notify();
                }
                Log.d(TAG, "RenderRunnable exit");
            }
        }

        protected void render() {
            Canvas canvas = null;
            int oldcnt = (mDynDrawRender != null) ? mDynDrawRender.getAppendCount() : 0;
            final SurfaceHolder holder = getHolder();

            try {
                canvas = holder.lockCanvas();
                if (mCanvasAdapter.beginPaint(canvas)) {
                    if (mBackground != null) {
                        mBackground.draw(canvas);
                    } else {
                        canvas.drawColor(mBkColor, mBkColor == Color.TRANSPARENT
                                ? Mode.CLEAR : Mode.SRC_OVER);
                    }
                    drawShapes(mCanvasAdapter);
                    mCanvasAdapter.endPaint();

                    if (mDynDrawRender != null)
                        mDynDrawRender.afterRegen(oldcnt);
                }
            } catch (Exception e) {
                e.printStackTrace();
            } finally {
                if (canvas != null) {
                    holder.unlockCanvasAndPost(canvas);
                }
            }
        }
    }

    protected class SurfaceCallback implements SurfaceHolder.Callback {
        @Override
        public void surfaceChanged(SurfaceHolder holder, int format, int width, int height) {
            mCoreView.onSize(mViewAdapter, width, height);

            postDelayed(new Runnable() {
                @Override
                public void run() {
                    mRender.requestRender();
                    removeCallbacks(this);
                }
            }, 100);
        }

        @Override
        public void surfaceCreated(SurfaceHolder holder) {
            final Canvas canvas = holder.lockCanvas();
            canvas.drawColor(mBkColor, mBkColor == Color.TRANSPARENT ? Mode.CLEAR : Mode.SRC_OVER);
            holder.unlockCanvasAndPost(canvas);

            mRender = new RenderRunnable();
            new Thread(mRender, "touchvg.render").start();
        }

        @Override
        public void surfaceDestroyed(SurfaceHolder holder) {
            mRender.stop();
            mRender = null;
        }
    }

    protected class DynRenderRunnable implements Runnable {
        private CanvasAdapter mDynDrawCanvas = new CanvasAdapter(null, mImageCache);
        private int[] mAppendShapeIDs = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public void requestRender() {
            synchronized (this) {
                this.notify();
            }
        }

        public void stop() {
            if (mDynDrawCanvas != null) {
                mViewAdapter.stop();
                requestRender();
                synchronized (mDynDrawCanvas) {
                    try {
                        mDynDrawCanvas.wait(1000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
                mDynDrawCanvas.delete();
                mDynDrawCanvas = null;
            }
        }

        public int getAppendCount() {
            int n = 0;
            for (int i = 0; i < mAppendShapeIDs.length; i++) {
                if (mAppendShapeIDs[i] != 0) {
                    n++;
                }
            }
            return n;
        }

        public void afterRegen(int count) {
            int maxCount = getAppendCount();
            count = Math.min(count, maxCount);
            for (int i = 0, j = count; i < mAppendShapeIDs.length; i++, j++) {
                mAppendShapeIDs[i] = j < mAppendShapeIDs.length ? mAppendShapeIDs[j] : 0;
            }
            requestRender();
        }

        public void requestAppendRender(int sid) {
            for (int i = 0; i < mAppendShapeIDs.length; i++) {
                if (mAppendShapeIDs[i] == sid)
                    break;
                if (mAppendShapeIDs[i] == 0) {
                    mAppendShapeIDs[i] = sid;
                    break;
                }
            }
            requestRender();
        }

        @Override
        public void run() {
            while (!mViewAdapter.isStopping()) {
                synchronized (this) {
                    try {
                        this.wait();
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
                if (!mViewAdapter.isStopping())
                    render();
            }
            if (mDynDrawCanvas != null) {
                synchronized (mDynDrawCanvas) {
                    mDynDrawCanvas.notify();
                }
                Log.d(TAG, "DynRenderRunnable exit");
            }
        }

        protected void render() {
            final SurfaceHolder holder = mDynDrawView.getHolder();
            Canvas canvas = null;
            try {
                canvas = holder.lockCanvas();
                if (mDynDrawCanvas.beginPaint(canvas)) {
                    int doc, shapes, gs;

                    synchronized (mCoreView) {
                        doc = mAppendShapeIDs[0] != 0 ? mCoreView.acquireFrontDoc() : 0;
                        shapes = mCoreView.acquireDynamicShapes();
                        gs = mCoreView.acquireGraphics(mViewAdapter);
                    }
                    try {
                        canvas.drawColor(Color.TRANSPARENT, Mode.CLEAR);
                        for (int sid : mAppendShapeIDs) {
                            if (sid != 0) {
                                mCoreView.drawAppend(doc, gs, mDynDrawCanvas, sid);
                            }
                        }
                        mCoreView.dynDraw(shapes, gs, mDynDrawCanvas);
                    } finally {
                        GiCoreView.releaseDoc(doc);
                        GiCoreView.releaseShapes(shapes);
                        mCoreView.releaseGraphics(gs);
                        mDynDrawCanvas.endPaint();
                    }
                }
            } catch (Exception e) {
                e.printStackTrace();
            } finally {
                if (canvas != null) {
                    holder.unlockCanvasAndPost(canvas);
                }
            }
        }
    }

    protected class DynSurfaceCallback implements SurfaceHolder.Callback {
        @Override
        public void surfaceChanged(SurfaceHolder holder, int format, int width, int height) {
            mDynDrawRender.requestRender();
        }

        @Override
        public void surfaceCreated(SurfaceHolder holder) {
            mDynDrawRender = new DynRenderRunnable();
            new Thread(mDynDrawRender, "touchvg.dynrender").start();
        }

        @Override
        public void surfaceDestroyed(SurfaceHolder holder) {
            mDynDrawRender.stop();
            mDynDrawRender = null;
        }
    }

    protected class SFViewAdapter extends BaseViewAdapter {

        @Override
        protected GraphView getGraphView() {
            return SFGraphView.this;
        }

        @Override
        protected ContextAction createContextAction() {
            return new ContextAction(mCoreView, SFGraphView.this);
        }

        @Override
        public void regenAll(boolean changed) {
            if (!mCoreView.isPlaying()) {
                if (mCoreView.isUndoLoading()) {
                    if (mRecorder != null) {
                        int tick1 = mCoreView.getRecordTick(false);
                        int doc1 = changed ? mCoreView.acquireFrontDoc() : 0;
                        int shapes1 = mCoreView.acquireDynamicShapes();
                        mRecorder.requestRecord(tick1, doc1, shapes1);
                    }
                } else {
                    synchronized (mCoreView) {
                        if (changed)
                            mCoreView.submitBackDoc(mViewAdapter);
                        mCoreView.submitDynamicShapes(mViewAdapter);

                        if (mUndoing != null) {
                            int tick0 = mCoreView.getRecordTick(true);
                            int doc0 = changed ? mCoreView.acquireFrontDoc() : 0;
                            int shapes0 = mCoreView.acquireDynamicShapes();
                            mUndoing.requestRecord(tick0, doc0, shapes0);
                        }
                        if (mRecorder != null) {
                            int tick1 = mCoreView.getRecordTick(false);
                            int doc1 = changed ? mCoreView.acquireFrontDoc() : 0;
                            int shapes1 = mCoreView.acquireDynamicShapes();
                            mRecorder.requestRecord(tick1, doc1, shapes1);
                        }
                    }
                }
            }
            if (mRender != null) {
                mRender.requestRender();
            }
        }

        @Override
        public void regenAppend(int sid) {
            if (!mCoreView.isPlaying()) {
                synchronized (mCoreView) {
                    mCoreView.submitBackDoc(mViewAdapter);
                    mCoreView.submitDynamicShapes(mViewAdapter);

                    if (mUndoing != null) {
                        int tick0 = mCoreView.getRecordTick(true);
                        int doc0 = mCoreView.acquireFrontDoc();
                        int shapes0 = mCoreView.acquireDynamicShapes();
                        mUndoing.requestRecord(tick0, doc0, shapes0);
                    }
                    if (mRecorder != null) {
                        int tick1 = mCoreView.getRecordTick(false);
                        int doc1 = mCoreView.acquireFrontDoc();
                        int shapes1 = mCoreView.acquireDynamicShapes();
                        mRecorder.requestRecord(tick1, doc1, shapes1);
                    }
                }
            }
            if (mDynDrawRender != null) {
                mDynDrawRender.requestAppendRender(sid);
            }
            if (mRender != null) {
                mRender.requestRender();
            }
        }

        @Override
        public void redraw() {
            if (!mCoreView.isPlaying()) {
                synchronized (mCoreView) {
                    mCoreView.submitDynamicShapes(mViewAdapter);
                    if (mUndoing != null) {
                        int tick0 = mCoreView.getRecordTick(true);
                        int shapes0 = mCoreView.acquireDynamicShapes();
                        mUndoing.requestRecord(tick0, 0, shapes0);
                    }
                    if (mRecorder != null) {
                        int tick1 = mCoreView.getRecordTick(false);
                        int shapes1 = mCoreView.acquireDynamicShapes();
                        mRecorder.requestRecord(tick1, 0, shapes1);
                    }
                }
            }
            if (mDynDrawRender != null) {
                mDynDrawRender.requestRender();
            }
        }
    }

    @Override
    public GiCoreView coreView() {
        return mCoreView;
    }

    @Override
    public GiView viewAdapter() {
        return mViewAdapter;
    }

    @Override
    public View getView() {
        return this;
    }

    @Override
    public View createDynamicShapeView(Context context) {
        if (mDynDrawView == null) {
            mDynDrawView = new SurfaceView(context);

            mDynDrawView.getHolder().setFormat(PixelFormat.TRANSPARENT);
            mDynDrawView.getHolder().addCallback(new DynSurfaceCallback());
            mDynDrawView.setZOrderOnTop(true);
        }
        return mDynDrawView;
    }

    @Override
    public ImageCache getImageCache() {
        return mImageCache;
    }

    @Override
    public void clearCachedData() {
        mCoreView.clearCachedData();
    }

    @Override
    public void onPause() {
        mViewAdapter.stop();
        if (mRender != null) {
            mRender.stop();
        }
        if (mDynDrawRender != null) {
            mDynDrawRender.stop();
        }
        mDynDrawView = null;
    }

    @Override
    public void setBackgroundColor(int color) {
        boolean transparent = (color == Color.TRANSPARENT);
        mBkColor = color;
        getHolder().setFormat(transparent ? PixelFormat.TRANSPARENT : PixelFormat.OPAQUE);
        if (!transparent) {
            mCoreView.setBkColor(mViewAdapter, color);
        }
        mViewAdapter.regenAll(false);
    }

    @Override
    public void setBackgroundDrawable(Drawable background) {
        this.mBackground = background;
    }

    @Override
    public void setContextActionEnabled(boolean enabled) {
        mViewAdapter.setContextActionEnabled(enabled);
    }

    @Override
    public void setGestureEnable(boolean enabled) {
        mGestureEnable = enabled;
        mGestureListener.setGestureEnable(enabled);
    }

    @Override
    public boolean onTouch(int action, float x, float y) {
        return mGestureListener.onTouch(this, action, x, y);
    }

    @Override
    public boolean onTap(float x, float y) {
        return mGestureListener.onTap(x, y);
    }

    @Override
    public Bitmap snapshot(boolean transparent) {
        final Bitmap bitmap = Bitmap.createBitmap(getWidth(), getHeight(), Bitmap.Config.ARGB_8888);
        final CanvasAdapter canvasAdapter = new CanvasAdapter(null, mImageCache);

        if (canvasAdapter.beginPaint(new Canvas(bitmap))) {
            if (transparent) {
                bitmap.eraseColor(Color.TRANSPARENT);
            } else {
                bitmap.eraseColor(mBkColor);
                if (mBackground != null) {
                    mBackground.draw(canvasAdapter.getCanvas());
                }
            }
            drawShapes(canvasAdapter);
            canvasAdapter.endPaint();
        }
        canvasAdapter.delete();

        return bitmap;
    }

    @Override
    public void setOnCommandChangedListener(CommandChangedListener listener) {
        mViewAdapter.setOnCommandChangedListener(listener);
    }

    @Override
    public void setOnSelectionChangedListener(SelectionChangedListener listener) {
        mViewAdapter.setOnSelectionChangedListener(listener);
    }

    @Override
    public void setOnContentChangedListener(ContentChangedListener listener) {
        mViewAdapter.setOnContentChangedListener(listener);
    }

    @Override
    public void setOnDynamicChangedListener(DynamicChangedListener listener) {
        mViewAdapter.setOnDynamicChangedListener(listener);
    }
}
