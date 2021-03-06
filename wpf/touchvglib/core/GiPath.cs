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

public class GiPath : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GiPath(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(GiPath obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~GiPath() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          touchvgPINVOKE.delete_GiPath(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public GiPath() : this(touchvgPINVOKE.new_GiPath__SWIG_0(), true) {
  }

  public GiPath(GiPath src) : this(touchvgPINVOKE.new_GiPath__SWIG_1(GiPath.getCPtr(src)), true) {
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
  }

  public GiPath(int count, Point2d points, string types) : this(touchvgPINVOKE.new_GiPath__SWIG_2(count, Point2d.getCPtr(points), types), true) {
  }

  public GiPath copy(GiPath src) {
    GiPath ret = new GiPath(touchvgPINVOKE.GiPath_copy(swigCPtr, GiPath.getCPtr(src)), false);
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool genericRoundLines(int count, Point2d points, float radius, bool closed) {
    bool ret = touchvgPINVOKE.GiPath_genericRoundLines__SWIG_0(swigCPtr, count, Point2d.getCPtr(points), radius, closed);
    return ret;
  }

  public bool genericRoundLines(int count, Point2d points, float radius) {
    bool ret = touchvgPINVOKE.GiPath_genericRoundLines__SWIG_1(swigCPtr, count, Point2d.getCPtr(points), radius);
    return ret;
  }

  public int getCount() {
    int ret = touchvgPINVOKE.GiPath_getCount(swigCPtr);
    return ret;
  }

  public Point2d getPoints() {
    IntPtr cPtr = touchvgPINVOKE.GiPath_getPoints(swigCPtr);
    Point2d ret = (cPtr == IntPtr.Zero) ? null : new Point2d(cPtr, false);
    return ret;
  }

  public string getTypes() {
    string ret = touchvgPINVOKE.GiPath_getTypes(swigCPtr);
    return ret;
  }

  public void clear() {
    touchvgPINVOKE.GiPath_clear(swigCPtr);
  }

  public void transform(Matrix2d mat) {
    touchvgPINVOKE.GiPath_transform(swigCPtr, Matrix2d.getCPtr(mat));
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
  }

  public void startFigure() {
    touchvgPINVOKE.GiPath_startFigure(swigCPtr);
  }

  public bool moveTo(Point2d point) {
    bool ret = touchvgPINVOKE.GiPath_moveTo(swigCPtr, Point2d.getCPtr(point));
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool lineTo(Point2d point) {
    bool ret = touchvgPINVOKE.GiPath_lineTo(swigCPtr, Point2d.getCPtr(point));
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool linesTo(int count, Point2d points) {
    bool ret = touchvgPINVOKE.GiPath_linesTo(swigCPtr, count, Point2d.getCPtr(points));
    return ret;
  }

  public bool beziersTo(int count, Point2d points, bool reverse) {
    bool ret = touchvgPINVOKE.GiPath_beziersTo__SWIG_0(swigCPtr, count, Point2d.getCPtr(points), reverse);
    return ret;
  }

  public bool beziersTo(int count, Point2d points) {
    bool ret = touchvgPINVOKE.GiPath_beziersTo__SWIG_1(swigCPtr, count, Point2d.getCPtr(points));
    return ret;
  }

  public bool arcTo(Point2d point) {
    bool ret = touchvgPINVOKE.GiPath_arcTo__SWIG_0(swigCPtr, Point2d.getCPtr(point));
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool arcTo(Point2d point, Point2d end) {
    bool ret = touchvgPINVOKE.GiPath_arcTo__SWIG_1(swigCPtr, Point2d.getCPtr(point), Point2d.getCPtr(end));
    if (touchvgPINVOKE.SWIGPendingException.Pending) throw touchvgPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool closeFigure() {
    bool ret = touchvgPINVOKE.GiPath_closeFigure(swigCPtr);
    return ret;
  }

}

}
