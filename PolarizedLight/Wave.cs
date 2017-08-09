using System;
using Tao.OpenGl;

namespace PolarizedLight
{
    class Wave
    {
        // Параметры отображения и Vertex Buffer Object'ы (VBO) +++
        public struct DrawFlags { public bool OutLine, Vectors, Y, Z, Sum; }
        public DrawFlags Draw;                      // Какие элементы рисовать
        private int[] VBOPtr = new int[7];
        private float[] SumVBO, YVBO, ZVBO, SumVecVBO, YVecVBO, ZVecVBO, BeamVBO;
        private double GridStep = 0.15;             // Шаг разбиения
        private int VecSpacing = 3;                 // Через сколько шагов рисовать векторы
        private int Steps, VecSteps;                // Кол-во шагов сетки
        private float[] SumColor = new float[4], YColor = new float[4], ZColor = new float[4];
        public float[] Color
        {
            get { return SumColor; }
            set
            {
                value.CopyTo(SumColor, 0);
                for (int i = 0; i < 4; i++)
                {
                    YColor[i] = value[i] * 0.7f;
                    ZColor[i] = value[i] * 0.4f;
                }
            }
        }
        
        // Параметры волны ++++++++++++++++++++++++++++++++++++++++
        private double X0, Length;                  // Начальная координата и ОБЩАЯ длина волны
        private float Ey, Ez;                       // Амплитуды
        private double ny, nz;                      // Коэф-ты преломления
        private double Lambda, Phi0Y, Phi0Z;        // Длина волны, нач. фазы
        private double c = 6.0;               // Скорость света
        private double DeltaPhi;                    // Разность фаз

        // Вспомогательное ++++++++++++++++++++++++++++++++++++++++
        private double P;                           // P = 2*Pi/Lambda
        public double t = 0.0;                      // Время
        private double CurrentPhi0Y = 0.0;          // Текущая фаза в начале сегмента

        #region конструкторы
        // Конструктор начального сегмента волны
        public Wave(double WaveLen, double DPhi, float E_y, float E_z, double n_y, double n_z, double _X0, double _Length, double _c)
        {
            Lambda = WaveLen; DeltaPhi = DPhi;
            Ey = E_y; Ez = E_z;
            ny = n_y; nz = n_z;
            X0 = _X0; Length = _Length;
            c = _c;

            Draw.OutLine = false; Draw.Vectors = false;
            Draw.Sum = false; Draw.Y = false; Draw.Z = false;

            BeamVBO = new float[] {
                (float)X0, 0.0f, 0.0f,
                (float)(X0 + Length), 0.0f, 0.0f
            };

            Steps = (int)(Length / GridStep);
            SumVBO = new float[Steps * 3 + 3];
            YVBO = new float[Steps * 3 + 3];
            ZVBO = new float[Steps * 3 + 3];
            
            Color = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };

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

            Gl.glGenBuffers(7, VBOPtr);

            BeamVBO = new float[] {
                (float)X0, 0.0f, 0.0f,
                (float)(X0 + Length), 0.0f, 0.0f
            };
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[6]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(BeamVBO.Length * sizeof(float)), BeamVBO, Gl.GL_STATIC_DRAW);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);

            P = 2.0 * Math.PI / Lambda;
            Phi0Y = -P * (-ny * X0);
            Phi0Z = Phi0Y + DeltaPhi;
        }

        // Конструктор последующего сегмента волны
        // Передаем предыдущий сегмент новые ny, nz и длину
        public Wave(Wave W, double n_y, double n_z, double _Length)
        {
            Lambda = W.Lambda; DeltaPhi = 0.0;
            Ey = W.Ey; Ez = W.Ez;
            ny = n_y; nz = n_z;
            X0 = W.X0 + W.Length; Length = _Length;
            c = W.c;

            Draw.OutLine = false; Draw.Vectors = false;
            Draw.Sum = false; Draw.Y = false; Draw.Z = false;

            Steps = (int)(Length / GridStep);
            SumVBO = new float[Steps * 3 + 3];
            YVBO = new float[Steps * 3 + 3];
            ZVBO = new float[Steps * 3 + 3];
            
            Color = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };

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

            Gl.glGenBuffers(7, VBOPtr);

            BeamVBO = new float[] {
                (float)X0, 0.0f, 0.0f,
                (float)(X0 + Length), 0.0f, 0.0f
            };
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[6]);
            Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(BeamVBO.Length * sizeof(float)), BeamVBO, Gl.GL_STATIC_DRAW);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);

            P = 2.0 * Math.PI / Lambda;
            Phi0Y = W.GetEndPhi0Y() + P * (ny * X0);
            Phi0Z = W.GetEndPhi0Z() + P * (nz * X0);
        }
        #endregion

        #region Методы обновления параметров
        // Текущие фазы на конце сегмента -------------------------
        private double GetEndPhi0Y()
        {
            return P * (c * t - ny * (X0 + Length)) + Phi0Y;
        }

        private double GetEndPhi0Z()
        {
            return P * (c * t - nz * (X0 + Length)) + Phi0Z;
        }
        
        // Текущая фаза в начале сегмента
        public void FixCurrentPhase()
        {
            CurrentPhi0Y = P * (c * t - ny * X0) + Phi0Y;
        }

        // Длина волны --------------------------------------------
        // Для первого сегмента
        public void Lambda_update(double new_Lambda)
        {
            Lambda = new_Lambda;
            P = 2.0 * Math.PI / Lambda;
            Phi0Y = -P * (c * t - ny * X0) + CurrentPhi0Y;
            Phi0Z = Phi0Y + DeltaPhi;

            //SumColor = LambdaToRGB(Lambda);
            for (int i = 0; i < 3; i++)
            {
                YColor[i] = SumColor[i] * 0.7f;
                ZColor[i] = SumColor[i] * 0.4f;
            }
        }

        // Для последующих сегментов
        public void Lambda_update(Wave W)
        {
            Lambda = W.Lambda;
            P = 2.0 * Math.PI / Lambda;
            Phases_update(W);

            SumColor = W.SumColor;
            YColor = W.YColor;
            ZColor = W.ZColor;
        }

        // Коэф-ты преломления ------------------------------------
        public void ny_update(double new_ny)
        {
            ny = new_ny;
        }

        public void nz_update(double new_nz)
        {
            nz = new_nz;
        }

        // Фазы ---------------------------------------------------
        // Для первого сегмента 
        public void Phases_update(double new_DPhi)
        {
            DeltaPhi = new_DPhi;
            Phi0Z = Phi0Y + DeltaPhi;
        }

        // Для последующих сегментов
        public void Phases_update(Wave W)
        {
            Phi0Y = W.GetEndPhi0Y() - P * (c * t - ny * X0);
            Phi0Z = W.GetEndPhi0Z() - P * (c * t - nz * X0);
        }

        // Амплитуды ----------------------------------------------
        public void Ey_update(float new_Ey)
        {
            Ey = new_Ey;
        }

        public void Ez_update(float new_Ez)
        {
            Ez = new_Ez;
        }

        // Скорость -----------------------------------------------
        // Для первого сегмента 
        public void c_update(double new_c)
        {
            FixCurrentPhase();
            c = new_c;
            Phi0Y = -P * (c * t - ny * X0) + CurrentPhi0Y;
            Phi0Z = Phi0Y + DeltaPhi;
        }
        // Для последующих сегментов
        public void c_update(Wave W)
        {
            c = W.c;
            Phases_update(W);
        }
        #endregion

        public void render()
        {
            #region Расчеты
            double X; float Y; float Z; int i_offset, i_vec_offset;
            for (int i = 0; i < Steps; i++)
            {
                // Расчитываем огибающие
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

                // Расчитываем векторы
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

            // Отрисовка --------------------------------------------
            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);

            #region Луч
            Gl.glLineWidth(1.5f);
            Gl.glColor3fv(SumColor);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[6]);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            Gl.glDrawArrays(Gl.GL_LINES, 0, 2);
            #endregion

            #region Отрисовка огибающих
            if (Draw.OutLine)
            {
                Gl.glLineWidth(2.0f);
                if (Draw.Sum)
                {
                    Gl.glColor3fv(SumColor);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[0]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(SumVBO.Length * sizeof(float)), SumVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, Steps + 1);
                }

                if (Draw.Y)
                {
                    Gl.glColor3fv(YColor);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[1]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(YVBO.Length * sizeof(float)), YVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, Steps + 1);
                }

                if (Draw.Z)
                {
                    Gl.glColor3fv(ZColor);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[2]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(ZVBO.Length * sizeof(float)), ZVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, Steps + 1);
                }
            }
            #endregion
            
            #region Отрисовка векторов
            if (Draw.Vectors)
            {
                Gl.glLineWidth(1.5f);
                if (Draw.Sum)
                {
                    Gl.glColor3fv(SumColor);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[3]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(SumVecVBO.Length * sizeof(float)), SumVecVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINES, 0, VecSteps);
                }

                if (Draw.Y)
                {
                    Gl.glColor3fv(YColor);
                    Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, VBOPtr[4]);
                    Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)(YVecVBO.Length * sizeof(float)), YVecVBO, Gl.GL_DYNAMIC_DRAW);
                    Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                    Gl.glDrawArrays(Gl.GL_LINES, 0, VecSteps);
                }

                if (Draw.Z)
                {
                    Gl.glColor3fv(ZColor);
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
            // ------------------------------------------------------
        }
    }
}
