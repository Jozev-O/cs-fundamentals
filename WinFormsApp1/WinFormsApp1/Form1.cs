using System.Runtime.InteropServices;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Button startButton;
        private Button stopButton;
        private Label statusLabel;
        private bool isDragging = false;

        // Импорт Windows API функций
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        // Константы для mouse_event
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;

        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Настройка формы
            this.Text = "Drag Mouse Controller";
            this.Size = new Size(350, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Создание кнопки запуска
            startButton = new Button();
            startButton.Text = "Начать перетаскивание (F2)";
            startButton.Size = new Size(150, 30);
            startButton.Location = new Point(50, 50);
            startButton.Click += StartButton_Click;
            this.Controls.Add(startButton);

            // Создание кнопки остановки
            stopButton = new Button();
            stopButton.Text = "Остановить (F3)";
            stopButton.Size = new Size(150, 30);
            stopButton.Location = new Point(50, 90);
            stopButton.Enabled = false;
            stopButton.Click += StopButton_Click;
            this.Controls.Add(stopButton);

            // Статус
            statusLabel = new Label();
            statusLabel.Text = "Статус: Остановлено";
            statusLabel.Size = new Size(250, 20);
            statusLabel.Location = new Point(50, 130);
            this.Controls.Add(statusLabel);

            // Обработчик горячих клавиш
            this.KeyPreview = true;
            this.KeyDown += Form_KeyDown;

            // Таймер для проверки горячих клавиш
            var hotkeyTimer = new System.Windows.Forms.Timer();
            hotkeyTimer.Interval = 10;
            hotkeyTimer.Tick += HotkeyTimer_Tick;
            hotkeyTimer.Start();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartDragging();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopDragging();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                StartDragging();
            }
            else if (e.KeyCode == Keys.F3)
            {
                StopDragging();
            }
        }

        private void HotkeyTimer_Tick(object sender, EventArgs e)
        {
            // Проверка горячих клавиш глобально
            if (IsKeyPressed(Keys.F2))
            {
                StartDragging();
            }
            else if (IsKeyPressed(Keys.F3))
            {
                StopDragging();
            }
        }

        private bool IsKeyPressed(Keys key)
        {
            return (GetAsyncKeyState(key) & 0x8000) != 0;
        }

        private void StartDragging()
        {
            if (!isDragging)
            {
                isDragging = true;
                startButton.Enabled = false;
                stopButton.Enabled = true;
                statusLabel.Text = "Статус: Перетаскивание активно";
                this.Opacity = 0.7;

                // Запускаем перетаскивание в отдельном потоке
                System.Threading.Thread dragThread = new System.Threading.Thread(DragExecution);
                dragThread.IsBackground = true;
                dragThread.Start();
            }
        }

        private void StopDragging()
        {
            if (isDragging)
            {
                isDragging = false;
                startButton.Enabled = true;
                stopButton.Enabled = false;
                statusLabel.Text = "Статус: Остановлено";
                this.Opacity = 1.0;
            }
        }

        private void DragExecution()
        {
            try
            {
                // Нажимаем левую кнопку мыши (зажимаем)
                //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                //System.Threading.Thread.Sleep(100);

                // Получаем текущую позицию курсора
                Point currentPos = Cursor.Position;

                // Выполняем перемещение с зажатой кнопкой
                PerformDragMovement(currentPos);

                // Отпускаем кнопку мыши
                //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                // Гарантируем что кнопка мыши будет отпущена даже при ошибке
                //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Invoke(new Action(() =>
                {
                    statusLabel.Text = $"Ошибка: {ex.Message}";
                    StopDragging();
                }));
            }
        }

        private void PerformDragMovement(Point startPoint)
        {
            // Пример 1: Рисование квадрата с зажатой кнопкой
            //DrawSquareWhileDragging(startPoint, 100);

            // Пример 2: Перетаскивание по диагонали
            // DragDiagonal(startPoint, 200);

            // Пример 3: Рисование круга
            DrawCircleWhileDragging(startPoint, 100);
        }

        private void DrawSquareWhileDragging(Point start, int size)
        {
            // Перемещаемся в начальную точку (уже там, но для надежности)
            SetCursorPos(start.X, start.Y);
            System.Threading.Thread.Sleep(50);

            // Верхняя линия →
            for (int x = start.X; x <= start.X + size && isDragging; x += 3)
            {
                SetCursorPos(x, start.Y);
                System.Threading.Thread.Sleep(10);
            }

            // Правая линия ↓
            for (int y = start.Y; y <= start.Y + size && isDragging; y += 3)
            {
                SetCursorPos(start.X + size, y);
                System.Threading.Thread.Sleep(10);
            }

            // Нижняя линия ←
            for (int x = start.X + size; x >= start.X && isDragging; x -= 3)
            {
                SetCursorPos(x, start.Y + size);
                System.Threading.Thread.Sleep(10);
            }

            // Левая линия ↑
            for (int y = start.Y + size; y >= start.Y && isDragging; y -= 3)
            {
                SetCursorPos(start.X, y);
                System.Threading.Thread.Sleep(10);
            }
        }

        private void DragDiagonal(Point start, int distance)
        {
            // Простое перетаскивание по диагонали
            for (int i = 0; i <= distance && isDragging; i += 2)
            {
                SetCursorPos(start.X + i, start.Y + i);
                System.Threading.Thread.Sleep(15);
            }
        }

        private void DrawCircleWhileDragging(Point center, int radius)
        {
            int points = 360;
            for (int i = 0; i <= points && isDragging; i++)
            {
                double angle = 2 * Math.PI * i / points;
                int x = center.X + (int)(radius * Math.Cos(angle));
                int y = center.Y + (int)(radius * Math.Sin(angle));
                if (i > 0)
                    SetCursorPos(x, y);
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
