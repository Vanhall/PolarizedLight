using System;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Wave
    {
        private int[] VBOPtr = new int[3];
        private float[] SumVBO, YVBO, ZVBO;
        private double GridStep = 0.1;              // Шаг разбиения
        int steps;                                  // Кол-во шагов сетки

        // Начальная координата и ОБЩАЯ длина волны
        private double X0, Length;
        private float Ey, Ez;                       // Амплитуды
        public double ny, nz, nyz;                 // Коэф-ты преломления
        // Длина волны, нач. фаза по Z, разность фаз
        private double Lambda, Phi0Y, Phi0Z;
        private const double c = 6.0;                 // Скорость света
        private double P;                           // P = 2*Pi/Lambda
        public double t = 0.0;                      // Время

        #region конструкторы
        public Wave(double WaveLen, double DPhi, float E_y, float E_z, double _X0, double _Length)
        {
            Lambda = WaveLen;
            Ey = E_y; Ez = E_z;
            ny = 1.0; nz = 1.0;
            X0 = _X0; Length = _Length;

            steps = (int)(Length/GridStep);
            SumVBO = new float[steps * 3 + 3];
            YVBO = new float[steps * 3 + 3];
            ZVBO = new float[steps * 3 + 3];
            Gl.glGenBuffers(3, VBOPtr);

            P = 2.0 * Math.PI / Lambda;
            Phi0Y = - P * (- ny * X0);
            Phi0Z = DPhi + Phi0Y;
        }

        public Wave(double WaveLen, double DPhi, float E_y, float E_z, double n_y, double n_z, double _X0, double _Length)
        {
            Lambda = WaveLen;
            Ey = E_y; Ez = E_z;
            ny = n_y; nz = n_z;
            X0 = _X0; Length = _Length;

            steps = (int)(Length / GridStep);
            SumVBO = new float[steps * 3 + 3];
            YVBO = new float[steps * 3 + 3];
            ZVBO = new float[steps * 3 + 3];
            Gl.glGenBuffers(3, VBOPtr);

            P = 2.0 * Math.PI / Lambda;
            Phi0Y = -P * (-ny * X0);
            Phi0Z = DPhi + Phi0Y;
        }
        #endregion
        
        
        private double GetEndPhi0Y()
        {
            return P * ( - ny * (X0 + Length)) + Phi0Y;
        }

        private double GetEndPhi0Z()
        {
            return P * ( - nz * (X0 + Length)) + Phi0Z;
        }
        
        public void link(Wave w)
        {
            Phi0Y = w.GetEndPhi0Y() + P * (ny * X0);
            Phi0Z = w.GetEndPhi0Z() + P * (nz * X0);
        }

        public void render()
        {
            double X; float Y; float Z; int i_offset;
            for (int i = 0; i < steps; i++)
            {
                i_offset = i * 3;
                X = X0 + i * GridStep;
                SumVBO[i_offset] = (float)(X);
                YVBO[i_offset] = (float)(X);
                ZVBO[i_offset] = (float)(X);

                Y = Ey * (float)Math.Cos(P * (c * t - ny * X) + Phi0Y);
                SumVBO[i_offset + 1] = Y;
                YVBO[i_offset + 1] = Y;
                ZVBO[i_offset + 1] = 0.0f;

                //if (!no_nyz) DeltaPhi = P * X * nyz + Phi0Y;
                Z = Ez * (float)Math.Cos(P * (c * t - nz * X) + Phi0Z);
                SumVBO[i_offset + 2] = Z;
                YVBO[i_offset + 2] = 0.0f;
                ZVBO[i_offset + 2] = Z;
            }

            i_offset = steps * 3;
            X = X0 + Length;
            SumVBO[i_offset] = (float)(X);
            YVBO[i_offset] = (float)(X);
            ZVBO[i_offset] = (float)(X);

            Y = Ey * (float)Math.Cos(P * (c * t - ny * X) + Phi0Y);
            SumVBO[i_offset + 1] = Y;
            YVBO[i_offset + 1] = Y;
            ZVBO[i_offset + 1] = 0.0f;

            //if (!no_nyz) DeltaPhi = P * X * nyz + Phi0Y;
            Z = Ez * (float)Math.Cos(P * (c * t - nz * X) + Phi0Z);
            SumVBO[i_offset + 2] = Z;
            YVBO[i_offset + 2] = 0.0f;
            ZVBO[i_offset + 2] = Z;

            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glLineWidth(2.0f);
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);

            Gl.glColor3f(1.0f, 1.0f, 0.0f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(SumVBO.Length * sizeof(float)), SumVBO, Gl.GL_DYNAMIC_DRAW);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, steps + 1);

            Gl.glColor3f(1.0f, 0.0f, 1.0f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[1]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(YVBO.Length * sizeof(float)), YVBO, Gl.GL_DYNAMIC_DRAW);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, steps + 1);

            Gl.glColor3f(0.0f, 1.0f, 1.0f);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[2]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(ZVBO.Length * sizeof(float)), ZVBO, Gl.GL_DYNAMIC_DRAW);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, steps + 1);

            // Отключаем режим отрисовки VBO
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }
    }
}
