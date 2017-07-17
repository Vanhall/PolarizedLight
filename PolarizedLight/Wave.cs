using System;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Wave
    {
        public struct DrawFlags { public bool OutLine, Vectors, Y, Z, Sum;
        }
        public DrawFlags Draw;                      // Какие элементы рисовать
        private int[] VBOPtr = new int[6];
        private float[] SumVBO, YVBO, ZVBO, SumVecVBO, YVecVBO, ZVecVBO;
        private double GridStep = 0.1;              // Шаг разбиения
        private int VecSpacing = 5;                 // Через сколько шагов рисовать векторы
        int Steps, VecSteps;                        // Кол-во шагов сетки

        // Начальная координата и ОБЩАЯ длина волны
        private double X0, Length;
        private float Ey, Ez;                       // Амплитуды
        private double ny, nz;                      // Коэф-ты преломления
        private double Lambda, Phi0Y, Phi0Z;        // Длина волны, нач. фазы
        private const double c = 6.0;               // Скорость света

        private double P;                           // P = 2*Pi/Lambda
        public double t = 0.0;                      // Время

        #region конструкторы
        // Конструктор начального сегмента волны
        public Wave(double WaveLen, double DPhi, float E_y, float E_z, double n_y, double n_z, double _X0, double _Length)

        {
            Lambda = WaveLen;
            Ey = E_y; Ez = E_z;
            ny = n_y; nz = n_z;
            X0 = _X0; Length = _Length;

            Draw.OutLine = true; Draw.Vectors = true;
            Draw.Sum = true; Draw.Y = false; Draw.Z = false;

            Steps = (int)(Length / GridStep);
            SumVBO = new float[Steps * 3 + 3];
            YVBO = new float[Steps * 3 + 3];
            ZVBO = new float[Steps * 3 + 3];

            VecSteps = Steps * 2 / VecSpacing;
            SumVecVBO = new float[(Steps * 6 / VecSpacing) + 6];
            YVecVBO = new float[(Steps * 6 / VecSpacing) + 6];
            ZVecVBO = new float[(Steps * 6 / VecSpacing) + 6];
            for (int i = 0; i < VecSteps / 2; i++)
            {
                int i_offset = i * 6;
                float X = (float)(X0 + i * VecSpacing * GridStep);
                SumVecVBO[i_offset] = X;
                SumVecVBO[i_offset + 1] = 0.0f;
                SumVecVBO[i_offset + 2] = 0.0f;
                SumVecVBO[i_offset + 3] = X;

                YVecVBO[i_offset] = X;
                YVecVBO[i_offset + 1] = 0.0f;
                YVecVBO[i_offset + 2] = 0.0f;
                YVecVBO[i_offset + 3] = X;

                ZVecVBO[i_offset] = X;
                ZVecVBO[i_offset + 1] = 0.0f;
                ZVecVBO[i_offset + 2] = 0.0f;
                ZVecVBO[i_offset + 3] = X;
            }

            Gl.glGenBuffers(6, VBOPtr);

            P = 2.0 * Math.PI / Lambda;
            Phi0Y = -P * (-ny * X0);
            Phi0Z = DPhi + Phi0Y;
        }

        // Конструктор последующего сегмента волны
        // Передаем предыдущий сегмент и новые ny, nz
        public Wave(Wave W, double n_y, double n_z, double _Length)
        {
            Lambda = W.Lambda;
            Ey = W.Ey; Ez = W.Ez;
            ny = n_y; nz = n_z;
            X0 = W.X0 + W.Length; Length = _Length;

            Draw.OutLine = true; Draw.Vectors = true;
            Draw.Sum = true; Draw.Y = false; Draw.Z = false;

            Steps = (int)(Length / GridStep);
            SumVBO = new float[Steps * 3 + 3];
            YVBO = new float[Steps * 3 + 3];
            ZVBO = new float[Steps * 3 + 3];

            VecSteps = Steps * 2 / VecSpacing;
            SumVecVBO = new float[(Steps * 6 / VecSpacing) + 6];
            YVecVBO = new float[(Steps * 6 / VecSpacing) + 6];
            ZVecVBO = new float[(Steps * 6 / VecSpacing) + 6];
            for (int i = 0; i < VecSteps / 2; i++)
            {
                int i_offset = i * 6;
                float X = (float)(X0 + i * VecSpacing * GridStep);
                SumVecVBO[i_offset] = X;
                SumVecVBO[i_offset + 1] = 0.0f;
                SumVecVBO[i_offset + 2] = 0.0f;
                SumVecVBO[i_offset + 3] = X;

                YVecVBO[i_offset] = X;
                YVecVBO[i_offset + 1] = 0.0f;
                YVecVBO[i_offset + 2] = 0.0f;
                YVecVBO[i_offset + 3] = X;

                ZVecVBO[i_offset] = X;
                ZVecVBO[i_offset + 1] = 0.0f;
                ZVecVBO[i_offset + 2] = 0.0f;
                ZVecVBO[i_offset + 3] = X;
            }

            Gl.glGenBuffers(6, VBOPtr);

            P = 2.0 * Math.PI / Lambda;
            Phi0Y = W.GetEndPhi0Y() + P * (ny * X0);
            Phi0Z = W.GetEndPhi0Z() + P * (nz * X0);
        }
        #endregion
        
        private double GetEndPhi0Y()
        {
            return P * (c * t - ny * (X0 + Length)) + Phi0Y;
        }

        private double GetEndPhi0Z()
        {
            return P * (c * t - nz * (X0 + Length)) + Phi0Z;
        }
        
        public void Lambda_update(double new_Lambda)
        {
            Lambda = new_Lambda;
            P = 2.0 * Math.PI / Lambda;
        }

        public void Lambda_update(double new_Lambda, Wave W)
        {
            Lambda = new_Lambda;
            P = 2.0 * Math.PI / Lambda;
            Phases_update(W);
        }

        public void ny_update(double new_ny)
        {
            ny = new_ny;
        }

        public void nz_update(double new_nz)
        {
            nz = new_nz;
        }

        public void Phases_update(Wave W)
        {
            Phi0Y = W.GetEndPhi0Y() - P * (c * t - ny * X0);
            Phi0Z = W.GetEndPhi0Z() - P * (c * t - nz * X0);
        }

        public void Phases_update(double new_DPhi)
        {
            Phi0Y = - P * (c * t - ny * X0);
            Phi0Z = new_DPhi + Phi0Y;
        }

        public void Ey_update(float new_Ey)
        {
            Ey = new_Ey;
        }

        public void Ez_update(float new_Ez)
        {
            Ez = new_Ez;
        }

        public void render()
        {
            #region Рассчеты
            double X; float Y; float Z; int i_offset, i_vec_offset;
            for (int i = 0; i < Steps; i++)
            {
                // Рассчитываем огибающие
                i_offset = i * 3;
                X = X0 + i * GridStep;
                SumVBO[i_offset] = (float)(X);
                YVBO[i_offset] = (float)(X);
                ZVBO[i_offset] = (float)(X);

                Y = Ey * (float)Math.Cos(P * (c * t - ny * X) + Phi0Y);
                SumVBO[i_offset + 1] = Y;
                YVBO[i_offset + 1] = Y;
                ZVBO[i_offset + 1] = 0.0f;
                
                Z = Ez * (float)Math.Cos(P * (c * t - nz * X) + Phi0Z);
                SumVBO[i_offset + 2] = Z;
                YVBO[i_offset + 2] = 0.0f;
                ZVBO[i_offset + 2] = Z;

                // Рассчитываем векторы
                if (i % VecSpacing == 0)
                {
                    i_vec_offset = (i * 6 / VecSpacing) + 4;
                    SumVecVBO[i_vec_offset] = Y;
                    SumVecVBO[i_vec_offset + 1] = Z;

                    YVecVBO[i_vec_offset] = Y;
                    YVecVBO[i_vec_offset + 1] = 0.0f;

                    ZVecVBO[i_vec_offset] = 0.0f;
                    ZVecVBO[i_vec_offset + 1] = Z;
                }
            }

            // Последнюю точку огибающей считаем отдельно
            i_offset = Steps * 3;
            X = X0 + Length;
            SumVBO[i_offset] = (float)(X);
            YVBO[i_offset] = (float)(X);
            ZVBO[i_offset] = (float)(X);

            Y = Ey * (float)Math.Cos(P * (c * t - ny * X) + Phi0Y);
            SumVBO[i_offset + 1] = Y;
            YVBO[i_offset + 1] = Y;
            ZVBO[i_offset + 1] = 0.0f;
            
            Z = Ez * (float)Math.Cos(P * (c * t - nz * X) + Phi0Z);
            SumVBO[i_offset + 2] = Z;
            YVBO[i_offset + 2] = 0.0f;
            ZVBO[i_offset + 2] = Z;
            #endregion

            // Отрисовка ################################################
            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);

            // Огибающие --------------------------------------------
            #region Отрисовка огибающих
            if (Draw.OutLine)
            {
                Gl.glLineWidth(2.0f);
                if (Draw.Sum)
                {
                    Gl.glColor3f(1.0f, 1.0f, 0.0f);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(SumVBO.Length * sizeof(float)), SumVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, Steps + 1);
                }

                if (Draw.Y)
                {
                    Gl.glColor3f(1.0f, 0.0f, 1.0f);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[1]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(YVBO.Length * sizeof(float)), YVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, Steps + 1);
                }

                if (Draw.Z)
                {
                    Gl.glColor3f(0.0f, 1.0f, 1.0f);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[2]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(ZVBO.Length * sizeof(float)), ZVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, Steps + 1);
                }
            }
            #endregion

            // Векторы -----------------------------------------------
            #region Отрисовка векторов
            if (Draw.Vectors)
            {
                Gl.glLineWidth(1.5f);
                if (Draw.Sum)
                {
                    Gl.glColor3f(1.0f, 1.0f, 0.0f);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[3]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(SumVecVBO.Length * sizeof(float)), SumVecVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINES, 0, VecSteps);
                }

                if (Draw.Y)
                {
                    Gl.glColor3f(1.0f, 0.0f, 1.0f);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[4]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(YVecVBO.Length * sizeof(float)), YVecVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINES, 0, VecSteps);
                }

                if (Draw.Z)
                {
                    Gl.glColor3f(0.0f, 1.0f, 1.0f);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[5]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(ZVecVBO.Length * sizeof(float)), ZVecVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINES, 0, VecSteps);
                }
            }
            #endregion

            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }
    }
}
