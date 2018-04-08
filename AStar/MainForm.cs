using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStar
{
    public partial class MainForm : Form
    {
        public enum UpdateType { None, Init, Create, Build, Move };

        private bool _isCreated; // 맵 생성 여부
        private bool _isStarted; // 길찾기 시작 여부

        private int _mapSizeX;
        private int _mapSizeY;

        private List<Tile> _tiles;
        private List<Tile> _path;
        private List<Tile> _openList;
        private List<Tile> _closeList;
        private UpdateType _updateType;

        private Brush _backgroundBrush;
        private Brush _normalBrush;
        private Brush _blockBrush;
        private Brush _pathBrush;
        private Brush _textBrush;
        private Pen _pen;
        private Font _font;

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Control Event
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _mapSizeX = (int) numericUpDown_x.Value;
            _mapSizeY = (int) numericUpDown_y.Value;

            _tiles = new List<Tile>();
            _path = new List<Tile>();
            _openList = new List<Tile>();
            _closeList = new List<Tile>();

            _backgroundBrush = new SolidBrush(Color.Black);
            _normalBrush = new SolidBrush(Color.Gray);
            _blockBrush = new SolidBrush(Color.DarkSlateGray);
            _pathBrush = new SolidBrush(Color.Red);
            _textBrush = new SolidBrush(Color.Yellow);
            _pen = new Pen(Color.DarkGray);
            _font = new Font("맑은 고딕", 10);

            UpdateMap(UpdateType.Init);
        }

        private void numericUpDown_x_ValueChanged(object sender, EventArgs e)
        {
            if (_isStarted)
            {
                numericUpDown_x.Value = _mapSizeX;
            }
            else
            {
                _mapSizeX = (int) numericUpDown_x.Value;
            }
        }

        private void numericUpDown_y_ValueChanged(object sender, EventArgs e)
        {
            if (_isStarted)
            {
                numericUpDown_y.Value = _mapSizeY;
            }
            else
            {
                _mapSizeY = (int) numericUpDown_y.Value;
            }
        }

        private void button_createMap_Click(object sender, EventArgs e)
        {
            if (_isStarted) return;

            _isCreated = false;
            int width = (pictureBox_map.Size.Width - 10) / _mapSizeX;
            int height = (pictureBox_map.Size.Height - 10) / _mapSizeY;
            if (width < height) height = width;
            else if (height < width) width = height;

            _tiles.Clear();
            for (int x = 0; x < _mapSizeX; x++)
            {
                for (int y = 0; y < _mapSizeY; y++)
                {
                    int locWidth = (x + 1) * width;
                    int locHeight = (y + 1) * height;
                    var loc = new Tile()
                    {
                        X = x,
                        Y = y,
                        Region = new Rectangle(new Point(x * width, y * height), new Size(width, height)),
                    };
                    _tiles.Add(loc);
                }
            }

            UpdateMap(UpdateType.Create);
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (!_isCreated) return;

            // 기존 정보 초기화
            _tiles.ForEach(o => o.Text = null);
            _openList.Clear();
            _closeList.Clear();
            _path.Clear();

            var startTile = _tiles[0];
            var endTile = _tiles[_tiles.Count - 1];

            _openList.Add(startTile);
            Tile tile = null;
            do
            {
                if (_openList.Count == 0) break;

                tile = _openList.OrderBy(o => o.F).First();
                _openList.Remove(tile);
                _closeList.Add(tile);

                if (tile == endTile) break;

                foreach (var target in _tiles)
                {
                    if (target.IsBlock) continue;
                    if (_closeList.Contains(target)) continue;
                    if (!IsNearLoc(tile, target)) continue;

                    if (!_openList.Contains(target))
                    {
                        _openList.Add(target);
                        target.Execute(tile, endTile);
                    }
                    else
                    {
                        if (Tile.CalcGValue(tile, target) < target.G)
                        {
                            target.Execute(tile, endTile);
                        }
                    }
                }
            }
            while (tile != null);

            if (tile != endTile)
            {
                MessageBox.Show("길막힘");
                return;
            }

            do
            {
                _path.Add(tile);
                tile = tile.Parent;
            }
            while (tile != null);
            _path.Reverse();

            for (int i = 0; i < _path.Count; i++)
            {
                if (i == 0) _path[i].Text = "START";
                else if (i == _path.Count - 1) _path[i].Text = "END";
                else _path[i].Text = i.ToString();
            }

            _isStarted = true;
            UpdateMap(UpdateType.Move);
        }

        private void pictureBox_map_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_isCreated || _isStarted) return;

            int clickX = e.Location.X;
            int clickY = e.Location.Y;

            foreach (var loc in _tiles)
            {
                int minX = loc.Region.X;
                int maxX = loc.Region.X + loc.Region.Width;
                int minY = loc.Region.Y;
                int maxY = loc.Region.Y + loc.Region.Height;
                if (clickX >= minX && clickX <= maxX && clickY >= minY && clickY <= maxY)
                {
                    loc.IsBlock = !loc.IsBlock;
                    break;
                }
            }

            UpdateMap(UpdateType.Build);
        }

        private void pictureBox_map_Paint(object sender, PaintEventArgs e)
        {
            if (_updateType == UpdateType.None) return;

            switch (_updateType)
            {
                case UpdateType.Init:
                    string waitMsg = "1.맵 크기 설정\r\n2.Create 클릭\r\n3.맵에 마우스 왼쪽버튼을 클릭하여 장애물 생성\r\n4.Start 클릭";
                    int width = pictureBox_map.Size.Width - 10;
                    int height = pictureBox_map.Size.Height - 10;
                    if (width < height) height = width;
                    else if (height < width) width = height;
                    e.Graphics.FillRectangle(_backgroundBrush, new Rectangle(0, 0, width, height));
                    e.Graphics.DrawString(waitMsg, _font, _textBrush, 0, 0);
                    break;
                case UpdateType.Create:
                    foreach (var loc in _tiles)
                    {
                        e.Graphics.FillRectangle(_normalBrush, loc.Region);
                        e.Graphics.DrawRectangle(_pen, loc.Region);
                    }

                    _isCreated = true;
                    break;
                case UpdateType.Build:
                    foreach (var loc in _tiles)
                    {
                        if (loc.IsBlock) e.Graphics.FillRectangle(_blockBrush, loc.Region);
                        else e.Graphics.FillRectangle(_normalBrush, loc.Region);
                        e.Graphics.DrawRectangle(_pen, loc.Region);
                    }
                    break;
                case UpdateType.Move:
                    foreach (var loc in _tiles)
                    {
                        if (loc.IsBlock) e.Graphics.FillRectangle(_blockBrush, loc.Region);
                        else e.Graphics.FillRectangle(_normalBrush, loc.Region);
                        e.Graphics.DrawRectangle(_pen, loc.Region);

                        if (!string.IsNullOrWhiteSpace(loc.Text))
                        {
                            e.Graphics.DrawString(loc.Text, _font, _textBrush, loc.Region.X, loc.Region.Y);
                        }
                    }

                    foreach (var loc in _path)
                    {
                        e.Graphics.FillRectangle(_pathBrush, loc.Region);
                        e.Graphics.DrawRectangle(_pen, loc.Region);

                        if (!string.IsNullOrWhiteSpace(loc.Text))
                        {
                            e.Graphics.DrawString(loc.Text, _font, _textBrush, loc.Region.X, loc.Region.Y);
                        }
                    }

                    _isStarted = false;
                    break;
            }
            _updateType = UpdateType.None;
        }
        #endregion

        #region Private Method
        private void UpdateMap(UpdateType type)
        {
            _updateType = type;
            pictureBox_map.Invalidate();
        }

        private bool IsNearLoc(Tile srcLoc, Tile targetLoc)
        {
            int diffX = Math.Abs(srcLoc.X - targetLoc.X);
            int diffY = Math.Abs(srcLoc.Y - targetLoc.Y);
            return diffX <= 1 && diffY <= 1;
        }
        #endregion

    }
}