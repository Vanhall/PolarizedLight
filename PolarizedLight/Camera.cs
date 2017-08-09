using System;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Camera
    {
        #region Вспомогательные функции
        private double ToRad(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double ToDeg(double radians)
        {
            return radians * 180.0 / Math.PI;
        }
        #endregion

        #region Константы и оганичители движения камеры
        private const double phiMax = 360.0, phiMin = 0.0;
        private const double psiMax = 89.9, psiMin = -15.0;
        private const double RMax = 30.0, RMin = 5.0;
        private const double ShiftMax = 18.0, ShiftMin = -18.0;
        #endregion

        private double[] eye = new double[3];
        private double[] pivot = new double[3] { 0, 0, 0 };
        private double[] up = new double[3] { 0, 0, 1 };

        #region параметры камеры
        public bool moving = false;
        public int mouseDX = 0;
        public int mouseDY = 0;

        private double _shift = 4.0;
        public double shift
        {
            get { return _shift; }
            set
            {
                if (value >= ShiftMax) _shift = ShiftMax;
                else if (value <= ShiftMin) _shift = ShiftMin;
                else _shift = value;
            }
        }
        // -------------------- сферические координаты ---------------------
        private double _phi = Math.PI / 4.0;
        public double phi
        {
            get { return ToDeg(_phi); }
            set
            {
                if (value >= phiMax) _phi = ToRad(value - 360);
                else if (value < phiMin) _phi = ToRad(value + 360);
                else _phi = ToRad(value);
            }
        }

        private double _psi = Math.PI / 6.0;
        public double psi
        {
            get { return ToDeg(_psi); }
            set
            {
                if (value >= psiMax) _psi = ToRad(psiMax);
                else if (value <= psiMin) _psi = ToRad(psiMin);
                else _psi = ToRad(value);
            }
        }

        private double _R = 25.0;
        public double R
        {
            get { return _R; }
            set
            {
                if (value <= RMin) _R = RMin;
                else if (value >= RMax) _R = RMax;
                else _R = value;
            }
        }
        #endregion

        // конструктор камеры
        public Camera()
        {
            eye[0] = R * Math.Cos(_phi) * Math.Cos(_psi);
            eye[1] = R * Math.Sin(_phi) * Math.Cos(_psi);
            eye[2] = R * Math.Sin(_psi);
            
            Gl.glLoadIdentity();
            Glu.gluLookAt(
                eye[0] + _shift, eye[1], eye[2],
                pivot[0] + _shift, pivot[1], pivot[2],
                up[0], up[1], up[2]);
        }

        #region Перемещение камеры
        public void rotate(double new_phi, double new_psi)
        {
            phi = new_phi; psi = new_psi;

            eye[0] = R * Math.Cos(_phi) * Math.Cos(_psi);
            eye[1] = R * Math.Sin(_phi) * Math.Cos(_psi);
            eye[2] = R * Math.Sin(_psi);

            Gl.glLoadIdentity();
            Glu.gluLookAt(
                eye[0] + _shift, eye[1], eye[2],
                pivot[0] + _shift, pivot[1], pivot[2],
                up[0], up[1], up[2]);
        }

        public void zoom(double new_R)
        {
            R = new_R;

            eye[0] = R * Math.Cos(_phi) * Math.Cos(_psi);
            eye[1] = R * Math.Sin(_phi) * Math.Cos(_psi);
            eye[2] = R * Math.Sin(_psi);

            Gl.glLoadIdentity();
            Glu.gluLookAt(
                eye[0] + _shift, eye[1], eye[2],
                pivot[0] + _shift, pivot[1], pivot[2],
                up[0], up[1], up[2]);
        }

        public void translate(double new_h)
        {
            shift = new_h;

            Gl.glLoadIdentity();
            Glu.gluLookAt(
                eye[0] + _shift, eye[1], eye[2],
                pivot[0] + _shift, pivot[1], pivot[2],
                up[0], up[1], up[2]);
        }

        public void reset()
        {
            phi = 45.0; psi = 30.0; R = 25.0; shift = 4.0;

            eye[0] = R * Math.Cos(_phi) * Math.Cos(_psi);
            eye[1] = R * Math.Sin(_phi) * Math.Cos(_psi);
            eye[2] = R * Math.Sin(_psi);

            Gl.glLoadIdentity();
            Glu.gluLookAt(
                eye[0] + _shift, eye[1], eye[2],
                pivot[0] + _shift, pivot[1], pivot[2],
                up[0], up[1], up[2]);
        }
        #endregion
    }
}
