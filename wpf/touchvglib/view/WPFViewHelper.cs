﻿//! \file WPFViewHelper.cs
//! \brief 定义WPF绘图视图辅助类
// Copyright (c) 2013, https://github.com/rhcad/touchvg

using System;
using System.Windows;
using System.Windows.Media;
using System.Text;
using touchvg.core;

namespace touchvg.view
{
    //! WPF绘图视图辅助类
    /*! \ingroup GROUP_WPF
     */
    public class WPFViewHelper : IDisposable
    {
        private static int LIB_RELEASE = 1; // TODO: 在本工程接口变化后增加此数
        private WPFGraphView View;
        private GiCoreView CoreView { get { return this.View.CoreView; } }
        public static WPFGraphView ActiveView { get { return WPFGraphView.ActiveView; } }

        public WPFViewHelper()
        {
            this.View = WPFGraphView.ActiveView;
        }

        public WPFViewHelper(WPFGraphView view)
        {
            this.View = view != null ? view : WPFGraphView.ActiveView;
        }

        public void Dispose()
        {
            if (this.View != null)
            {
                this.View.Dispose();
                this.View = null;
            }
        }

        //! 返回本库的版本号, 1.0.cslibver.corelibver
        public string Version { get {
            return string.Format("1.0.%d.%d", LIB_RELEASE, GiCoreView.getVersion());
        } }

        //! 返回内核视图的句柄, MgView 指针
        public int cmdViewHandle()
        {
            return CoreView.viewAdapterHandle();
        }

        //! 返回内核命令视图
        public MgView cmdView()
        {
            return MgView.fromHandle(cmdViewHandle());
        }

        //! 当前命令名称
        public string Command
        {
            get { return CoreView.getCommand(); }
            set { CoreView.setCommand(value); }
        }

        //! 指定名称和JSON串参数，启动命令
        public bool setCommand(string name, string param)
        {
            return CoreView.setCommand(name, param);
        }

        //! 线宽，正数表示0.1毫米单位，零表示1像素宽，负数表示像素单位
        public int LineWidth
        {
            get
            {
                float w = CoreView.getContext(false).getLineWidth();
                return (int)(w > 1e-5f ? w / 10.0f : w);
            }
            set
            {
                CoreView.getContext(true).setLineWidth(
                    value > 0 ? (float)value * 10.0f : value, true);
                CoreView.setContext((int)GiContextBits.kContextLineWidth);
            }
        }

        //! 像素单位的线宽，总是为正数
        public int StrokeWidth
        {
            get
            {
                GiContext ctx = CoreView.getContext(false);
                return (int)CoreView.calcPenWidth(View.ViewAdapter, ctx.getLineWidth());
            }
            set
            {
                CoreView.getContext(true).setLineWidth(
                    -Math.Abs((float)value), true);
                CoreView.setContext((int)GiContextBits.kContextLineWidth);
            }
        }

        //! 线型, 0-5:实线,虚线,点线,点划线,双点划线,空线
        public int LineStyle
        {
            get { return CoreView.getContext(false).getLineStyle(); }
            set
            {
                CoreView.getContext(true).setLineStyle(value);
                CoreView.setContext((int)GiContextBits.kContextLineStyle);
            }
        }

        //! 线条颜色，忽略透明度，Colors.Transparent不画线条
        public Color LineColor
        {
            get
            {
                GiColor c = CoreView.getContext(false).getLineColor();
                return c.a > 0 ? Color.FromRgb(c.r, c.g, c.b) : Colors.Transparent;
            }
            set
            {
                CoreView.getContext(true).setLineColor(
                    value.R, value.G, value.B, value.A);
                if (value.A > 0)
                    CoreView.setContext((int)GiContextBits.kContextLineRGB);
                else
                    CoreView.setContext((int)GiContextBits.kContextLineARGB);
            }
        }

        //! 线条透明度, 0-255
        public int LineAlpha
        {
            get { return CoreView.getContext(false).getLineAlpha(); }
            set
            {
                CoreView.getContext(true).setLineAlpha(value);
                CoreView.setContext((int)GiContextBits.kContextLineAlpha);
            }
        }

        //! 填充颜色，忽略透明度，Colors.Transparent不填充
        public Color FillColor
        {
            get
            {
                GiColor c = CoreView.getContext(false).getFillColor();
                return c.a > 0 ? Color.FromRgb(c.r, c.g, c.b) : Colors.Transparent;
            }
            set
            {
                CoreView.getContext(true).setFillColor(
                    value.R, value.G, value.B, value.A);
                if (value.A > 0)
                    CoreView.setContext((int)GiContextBits.kContextFillRGB);
                else
                    CoreView.setContext((int)GiContextBits.kContextFillARGB);
            }
        }

        //! 填充透明度, 0-255
        public int FillAlpha
        {
            get { return CoreView.getContext(false).getFillAlpha(); }
            set
            {
                CoreView.getContext(true).setFillAlpha(value);
                CoreView.setContext((int)GiContextBits.kContextFillAlpha);
            }
        }

        //! 绘图属性是否正在动态修改. 拖动时先设为true，然后改变绘图属性，完成后设为false.
        public bool ContextEditing
        {
            set { CoreView.setContextEditing(value); }
        }

        //! 图形总数
        public int ShapeCount
        {
            get { return CoreView.getShapeCount(); }
        }

        //! 选中的图形个数
        public int SelectedCount
        {
            get { return CoreView.getSelectedShapeCount(); }
        }

        //! 当前选中的图形的ID，选中多个时只取第一个
        public int SelectedShapeID
        {
            get { return CoreView.getSelectedShapeID(); }
        }

        //! 选中的图形的类型, MgShapeType
        public int SelectedType
        {
            get { return CoreView.getSelectedShapeType(); }
        }

        //! 图形改变次数，可用于检查是否需要保存
        public int ChangeCount
        {
            get { return CoreView.getChangeCount(); }
        }

        //! 显示次数
        public int DrawCount
        {
            get { return CoreView.getDrawCount(); }
        }

        //! 图形显示范围
        public Rect DisplayExtent
        {
            get {
                Floats box = new Floats(4);
                if (CoreView.getDisplayExtent(box))
                {
                    return new Rect(box.get(0), box.get(1),
                        box.get(2) - box.get(0), box.get(3) - box.get(1));
                }
                return new Rect();
            }
        }

        //! 选择包络框
        public Rect BoundingBox
        {
            get {
                Floats box = new Floats(4);
                if (CoreView.getBoundingBox(box))
                {
                    return new Rect(box.get(0), box.get(1),
                        box.get(2) - box.get(0), box.get(3) - box.get(1));
                }
                return new Rect();
            }
        }

        //! 所有图形的JSON内容
        public string Content
        {
            get
            {
                string ret = CoreView.getContent();
                CoreView.freeContent();
                return ret;
            }
            set { CoreView.setContent(value); }
        }

        //! 导出静态图形到SVG文件
        public bool ExportSVG(string filename)
        {
            return CoreView.exportSVG(View.ViewAdapter, filename) > 0;
        }

        //! 放缩显示全部内容到视图区域
        public bool ZoomToExtent()
        {
            return CoreView.zoomToExtent();
        }

        //! 放缩显示指定范围到视图区域
        public bool ZoomToModel(float x, float y, double w, double h)
        {
            return CoreView.zoomToModel(x, y, (float)w, (float)h);
        }

        //! 添加测试图形
        public int AddShapesForTest()
        {
            return CoreView.addShapesForTest();
        }

        //! 从JSON文件中加载图形
        public bool Load(string vgfile)
        {
            return CoreView.loadFromFile(vgfile);
        }

        //! 从JSON文件中只读加载图形
        public bool Load(string vgfile, bool readOnly)
        {
            return CoreView.loadFromFile(vgfile, readOnly);
        }

        //! 保存图形到JSON文件
        public bool Save(string vgfile)
        {
            return CoreView.saveToFile(vgfile);
        }
        
        //! 清除所有图形
        public void clearShapes() {
            CoreView.clear();
        }
    }
}
