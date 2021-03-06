/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.11
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace touchvg.core {

using System;
using System.Runtime.InteropServices;

public class GiCoreView : MgCoreView {
  private HandleRef swigCPtr;

  internal GiCoreView(IntPtr cPtr, bool cMemoryOwn) : base(touchvgPINVOKE.GiCoreView_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(GiCoreView obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~GiCoreView() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          touchvgPINVOKE.delete_GiCoreView(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public GiCoreView(GiCoreView mainView) : this(touchvgPINVOKE.new_GiCoreView__SWIG_0(GiCoreView.getCPtr(mainView)), true) {
  }

  public GiCoreView() : this(touchvgPINVOKE.new_GiCoreView__SWIG_1(), true) {
  }

  public void createView(GiView view, int type) {
    touchvgPINVOKE.GiCoreView_createView__SWIG_0(swigCPtr, GiView.getCPtr(view), type);
  }

  public void createView(GiView view) {
    touchvgPINVOKE.GiCoreView_createView__SWIG_1(swigCPtr, GiView.getCPtr(view));
  }

  public void createMagnifierView(GiView newview, GiView mainView) {
    touchvgPINVOKE.GiCoreView_createMagnifierView(swigCPtr, GiView.getCPtr(newview), GiView.getCPtr(mainView));
  }

  public void destoryView(GiView view) {
    touchvgPINVOKE.GiCoreView_destoryView(swigCPtr, GiView.getCPtr(view));
  }

  public bool isDrawing() {
    bool ret = touchvgPINVOKE.GiCoreView_isDrawing(swigCPtr);
    return ret;
  }

  public int stopDrawing() {
    int ret = touchvgPINVOKE.GiCoreView_stopDrawing(swigCPtr);
    return ret;
  }

  public int acquireGraphics(GiView view) {
    int ret = touchvgPINVOKE.GiCoreView_acquireGraphics(swigCPtr, GiView.getCPtr(view));
    return ret;
  }

  public void releaseGraphics(int hGs) {
    touchvgPINVOKE.GiCoreView_releaseGraphics(swigCPtr, hGs);
  }

  public int drawAll(int hDoc, int hGs, GiCanvas canvas) {
    int ret = touchvgPINVOKE.GiCoreView_drawAll__SWIG_0(swigCPtr, hDoc, hGs, GiCanvas.getCPtr(canvas));
    return ret;
  }

  public int drawAppend(int hDoc, int hGs, GiCanvas canvas, int sid) {
    int ret = touchvgPINVOKE.GiCoreView_drawAppend__SWIG_0(swigCPtr, hDoc, hGs, GiCanvas.getCPtr(canvas), sid);
    return ret;
  }

  public int dynDraw(int hShapes, int hGs, GiCanvas canvas) {
    int ret = touchvgPINVOKE.GiCoreView_dynDraw__SWIG_0(swigCPtr, hShapes, hGs, GiCanvas.getCPtr(canvas));
    return ret;
  }

  public int drawAll(GiView view, GiCanvas canvas) {
    int ret = touchvgPINVOKE.GiCoreView_drawAll__SWIG_1(swigCPtr, GiView.getCPtr(view), GiCanvas.getCPtr(canvas));
    return ret;
  }

  public int drawAppend(GiView view, GiCanvas canvas, int sid) {
    int ret = touchvgPINVOKE.GiCoreView_drawAppend__SWIG_1(swigCPtr, GiView.getCPtr(view), GiCanvas.getCPtr(canvas), sid);
    return ret;
  }

  public int dynDraw(GiView view, GiCanvas canvas) {
    int ret = touchvgPINVOKE.GiCoreView_dynDraw__SWIG_1(swigCPtr, GiView.getCPtr(view), GiCanvas.getCPtr(canvas));
    return ret;
  }

  public int setBkColor(GiView view, int argb) {
    int ret = touchvgPINVOKE.GiCoreView_setBkColor(swigCPtr, GiView.getCPtr(view), argb);
    return ret;
  }

  public static void setScreenDpi(int dpi, float factor) {
    touchvgPINVOKE.GiCoreView_setScreenDpi__SWIG_0(dpi, factor);
  }

  public static void setScreenDpi(int dpi) {
    touchvgPINVOKE.GiCoreView_setScreenDpi__SWIG_1(dpi);
  }

  public void onSize(GiView view, int w, int h) {
    touchvgPINVOKE.GiCoreView_onSize(swigCPtr, GiView.getCPtr(view), w, h);
  }

  public void setPenWidthRange(GiView view, float minw, float maxw) {
    touchvgPINVOKE.GiCoreView_setPenWidthRange(swigCPtr, GiView.getCPtr(view), minw, maxw);
  }

  public bool onGesture(GiView view, GiGestureType type, GiGestureState state, float x, float y, bool switchGesture) {
    bool ret = touchvgPINVOKE.GiCoreView_onGesture__SWIG_0(swigCPtr, GiView.getCPtr(view), (int)type, (int)state, x, y, switchGesture);
    return ret;
  }

  public bool onGesture(GiView view, GiGestureType type, GiGestureState state, float x, float y) {
    bool ret = touchvgPINVOKE.GiCoreView_onGesture__SWIG_1(swigCPtr, GiView.getCPtr(view), (int)type, (int)state, x, y);
    return ret;
  }

  public bool twoFingersMove(GiView view, GiGestureState state, float x1, float y1, float x2, float y2, bool switchGesture) {
    bool ret = touchvgPINVOKE.GiCoreView_twoFingersMove__SWIG_0(swigCPtr, GiView.getCPtr(view), (int)state, x1, y1, x2, y2, switchGesture);
    return ret;
  }

  public bool twoFingersMove(GiView view, GiGestureState state, float x1, float y1, float x2, float y2) {
    bool ret = touchvgPINVOKE.GiCoreView_twoFingersMove__SWIG_1(swigCPtr, GiView.getCPtr(view), (int)state, x1, y1, x2, y2);
    return ret;
  }

  public bool submitBackDoc(GiView view) {
    bool ret = touchvgPINVOKE.GiCoreView_submitBackDoc(swigCPtr, GiView.getCPtr(view));
    return ret;
  }

  public bool submitDynamicShapes(GiView view) {
    bool ret = touchvgPINVOKE.GiCoreView_submitDynamicShapes(swigCPtr, GiView.getCPtr(view));
    return ret;
  }

  public float calcPenWidth(GiView view, float lineWidth) {
    float ret = touchvgPINVOKE.GiCoreView_calcPenWidth(swigCPtr, GiView.getCPtr(view), lineWidth);
    return ret;
  }

  public GiGestureType getGestureType() {
    GiGestureType ret = (GiGestureType)touchvgPINVOKE.GiCoreView_getGestureType(swigCPtr);
    return ret;
  }

  public GiGestureState getGestureState() {
    GiGestureState ret = (GiGestureState)touchvgPINVOKE.GiCoreView_getGestureState(swigCPtr);
    return ret;
  }

  public static int getVersion() {
    int ret = touchvgPINVOKE.GiCoreView_getVersion();
    return ret;
  }

  public int exportSVG(int hDoc, int hGs, string filename) {
    int ret = touchvgPINVOKE.GiCoreView_exportSVG__SWIG_0(swigCPtr, hDoc, hGs, filename);
    return ret;
  }

  public int exportSVG(GiView view, string filename) {
    int ret = touchvgPINVOKE.GiCoreView_exportSVG__SWIG_1(swigCPtr, GiView.getCPtr(view), filename);
    return ret;
  }

  public bool startRecord(string path, int doc, bool forUndo) {
    bool ret = touchvgPINVOKE.GiCoreView_startRecord(swigCPtr, path, doc, forUndo);
    return ret;
  }

  public void stopRecord(bool forUndo) {
    touchvgPINVOKE.GiCoreView_stopRecord(swigCPtr, forUndo);
  }

  public bool recordShapes(bool forUndo, int tick, int doc, int shapes) {
    bool ret = touchvgPINVOKE.GiCoreView_recordShapes(swigCPtr, forUndo, tick, doc, shapes);
    return ret;
  }

  public bool undo(GiView view) {
    bool ret = touchvgPINVOKE.GiCoreView_undo(swigCPtr, GiView.getCPtr(view));
    return ret;
  }

  public bool redo(GiView view) {
    bool ret = touchvgPINVOKE.GiCoreView_redo(swigCPtr, GiView.getCPtr(view));
    return ret;
  }

  public static bool loadFrameIndex(string path, Ints arr) {
    bool ret = touchvgPINVOKE.GiCoreView_loadFrameIndex(path, Ints.getCPtr(arr));
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool isPressDragging() {
    bool ret = touchvgPINVOKE.GiCoreView_isPressDragging(swigCPtr);
    return ret;
  }

  public override bool isDrawingCommand() {
    bool ret = touchvgPINVOKE.GiCoreView_isDrawingCommand(swigCPtr);
    return ret;
  }

  public override int viewAdapterHandle() {
    int ret = touchvgPINVOKE.GiCoreView_viewAdapterHandle(swigCPtr);
    return ret;
  }

  public override int backDoc() {
    int ret = touchvgPINVOKE.GiCoreView_backDoc(swigCPtr);
    return ret;
  }

  public override int backShapes() {
    int ret = touchvgPINVOKE.GiCoreView_backShapes(swigCPtr);
    return ret;
  }

  public override int acquireFrontDoc() {
    int ret = touchvgPINVOKE.GiCoreView_acquireFrontDoc(swigCPtr);
    return ret;
  }

  public override int acquireDynamicShapes() {
    int ret = touchvgPINVOKE.GiCoreView_acquireDynamicShapes(swigCPtr);
    return ret;
  }

  public override bool isUndoRecording() {
    bool ret = touchvgPINVOKE.GiCoreView_isUndoRecording(swigCPtr);
    return ret;
  }

  public override bool isRecording() {
    bool ret = touchvgPINVOKE.GiCoreView_isRecording(swigCPtr);
    return ret;
  }

  public override bool isPlaying() {
    bool ret = touchvgPINVOKE.GiCoreView_isPlaying(swigCPtr);
    return ret;
  }

  public override int getRecordTick(bool forUndo) {
    int ret = touchvgPINVOKE.GiCoreView_getRecordTick(swigCPtr, forUndo);
    return ret;
  }

  public override bool isUndoLoading() {
    bool ret = touchvgPINVOKE.GiCoreView_isUndoLoading(swigCPtr);
    return ret;
  }

  public override bool canUndo() {
    bool ret = touchvgPINVOKE.GiCoreView_canUndo(swigCPtr);
    return ret;
  }

  public override bool canRedo() {
    bool ret = touchvgPINVOKE.GiCoreView_canRedo(swigCPtr);
    return ret;
  }

  public override int loadFirstFrame() {
    int ret = touchvgPINVOKE.GiCoreView_loadFirstFrame(swigCPtr);
    return ret;
  }

  public override int loadNextFrame(int index) {
    int ret = touchvgPINVOKE.GiCoreView_loadNextFrame(swigCPtr, index);
    return ret;
  }

  public override int loadPrevFrame(int index) {
    int ret = touchvgPINVOKE.GiCoreView_loadPrevFrame(swigCPtr, index);
    return ret;
  }

  public override void applyFrame(int flags) {
    touchvgPINVOKE.GiCoreView_applyFrame(swigCPtr, flags);
  }

  public override int getFrameIndex() {
    int ret = touchvgPINVOKE.GiCoreView_getFrameIndex(swigCPtr);
    return ret;
  }

  public override int getPlayingDocForEdit() {
    int ret = touchvgPINVOKE.GiCoreView_getPlayingDocForEdit(swigCPtr);
    return ret;
  }

  public override int getDynamicShapesForEdit() {
    int ret = touchvgPINVOKE.GiCoreView_getDynamicShapesForEdit(swigCPtr);
    return ret;
  }

  public override string getCommand() {
    string ret = touchvgPINVOKE.GiCoreView_getCommand(swigCPtr);
    return ret;
  }

  public override bool setCommand(string name, string arg1) {
    bool ret = touchvgPINVOKE.GiCoreView_setCommand__SWIG_0(swigCPtr, name, arg1);
    return ret;
  }

  public override bool setCommand(string name) {
    bool ret = touchvgPINVOKE.GiCoreView_setCommand__SWIG_1(swigCPtr, name);
    return ret;
  }

  public override bool doContextAction(int action) {
    bool ret = touchvgPINVOKE.GiCoreView_doContextAction(swigCPtr, action);
    return ret;
  }

  public override void clearCachedData() {
    touchvgPINVOKE.GiCoreView_clearCachedData(swigCPtr);
  }

  public override int addShapesForTest() {
    int ret = touchvgPINVOKE.GiCoreView_addShapesForTest(swigCPtr);
    return ret;
  }

  public override int getShapeCount() {
    int ret = touchvgPINVOKE.GiCoreView_getShapeCount__SWIG_0(swigCPtr);
    return ret;
  }

  public override int getShapeCount(int hDoc) {
    int ret = touchvgPINVOKE.GiCoreView_getShapeCount__SWIG_1(swigCPtr, hDoc);
    return ret;
  }

  public override int getChangeCount() {
    int ret = touchvgPINVOKE.GiCoreView_getChangeCount(swigCPtr);
    return ret;
  }

  public override int getDrawCount() {
    int ret = touchvgPINVOKE.GiCoreView_getDrawCount(swigCPtr);
    return ret;
  }

  public override int getSelectedShapeCount() {
    int ret = touchvgPINVOKE.GiCoreView_getSelectedShapeCount(swigCPtr);
    return ret;
  }

  public override int getSelectedShapeType() {
    int ret = touchvgPINVOKE.GiCoreView_getSelectedShapeType(swigCPtr);
    return ret;
  }

  public override int getSelectedShapeID() {
    int ret = touchvgPINVOKE.GiCoreView_getSelectedShapeID(swigCPtr);
    return ret;
  }

  public override void clear() {
    touchvgPINVOKE.GiCoreView_clear(swigCPtr);
  }

  public override bool loadFromFile(string vgfile, bool readOnly) {
    bool ret = touchvgPINVOKE.GiCoreView_loadFromFile__SWIG_0(swigCPtr, vgfile, readOnly);
    return ret;
  }

  public override bool loadFromFile(string vgfile) {
    bool ret = touchvgPINVOKE.GiCoreView_loadFromFile__SWIG_1(swigCPtr, vgfile);
    return ret;
  }

  public override bool saveToFile(int hDoc, string vgfile, bool pretty) {
    bool ret = touchvgPINVOKE.GiCoreView_saveToFile__SWIG_0(swigCPtr, hDoc, vgfile, pretty);
    return ret;
  }

  public override bool saveToFile(int hDoc, string vgfile) {
    bool ret = touchvgPINVOKE.GiCoreView_saveToFile__SWIG_1(swigCPtr, hDoc, vgfile);
    return ret;
  }

  public override bool loadShapes(MgStorage s, bool readOnly) {
    bool ret = touchvgPINVOKE.GiCoreView_loadShapes__SWIG_0(swigCPtr, MgStorage.getCPtr(s), readOnly);
    return ret;
  }

  public override bool loadShapes(MgStorage s) {
    bool ret = touchvgPINVOKE.GiCoreView_loadShapes__SWIG_1(swigCPtr, MgStorage.getCPtr(s));
    return ret;
  }

  public override bool saveShapes(int hDoc, MgStorage s) {
    bool ret = touchvgPINVOKE.GiCoreView_saveShapes(swigCPtr, hDoc, MgStorage.getCPtr(s));
    return ret;
  }

  public override string getContent(int hDoc) {
    string ret = touchvgPINVOKE.GiCoreView_getContent(swigCPtr, hDoc);
    return ret;
  }

  public override void freeContent() {
    touchvgPINVOKE.GiCoreView_freeContent(swigCPtr);
  }

  public override bool setContent(string content) {
    bool ret = touchvgPINVOKE.GiCoreView_setContent(swigCPtr, content);
    return ret;
  }

  public override bool zoomToExtent() {
    bool ret = touchvgPINVOKE.GiCoreView_zoomToExtent(swigCPtr);
    return ret;
  }

  public override bool zoomToModel(float x, float y, float w, float h) {
    bool ret = touchvgPINVOKE.GiCoreView_zoomToModel(swigCPtr, x, y, w, h);
    return ret;
  }

  public override GiContext getContext(bool forChange) {
    GiContext ret = new GiContext(touchvgPINVOKE.GiCoreView_getContext(swigCPtr, forChange), false);
    return ret;
  }

  public override void setContext(GiContext ctx, int mask, int apply) {
    touchvgPINVOKE.GiCoreView_setContext__SWIG_0(swigCPtr, GiContext.getCPtr(ctx), mask, apply);
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void setContext(int mask) {
    touchvgPINVOKE.GiCoreView_setContext__SWIG_1(swigCPtr, mask);
  }

  public override void setContextEditing(bool editing) {
    touchvgPINVOKE.GiCoreView_setContextEditing(swigCPtr, editing);
  }

  public override int addImageShape(string name, float width, float height) {
    int ret = touchvgPINVOKE.GiCoreView_addImageShape__SWIG_0(swigCPtr, name, width, height);
    return ret;
  }

  public override int addImageShape(string name, float xc, float yc, float w, float h) {
    int ret = touchvgPINVOKE.GiCoreView_addImageShape__SWIG_1(swigCPtr, name, xc, yc, w, h);
    return ret;
  }

  public override bool getDisplayExtent(Floats box) {
    bool ret = touchvgPINVOKE.GiCoreView_getDisplayExtent(swigCPtr, Floats.getCPtr(box));
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool getBoundingBox(Floats box) {
    bool ret = touchvgPINVOKE.GiCoreView_getBoundingBox__SWIG_0(swigCPtr, Floats.getCPtr(box));
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool getBoundingBox(Floats box, int shapeId) {
    bool ret = touchvgPINVOKE.GiCoreView_getBoundingBox__SWIG_1(swigCPtr, Floats.getCPtr(box), shapeId);
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
