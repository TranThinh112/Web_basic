using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagement.Utils
{
    /// <summary>
    /// Helper class for creating pagination controls
    /// </summary>
    public class PaginationHelper
    {
        public const int DEFAULT_PAGE_SIZE = 20;

        private Panel _paginationPanel;
        private Label _lblInfo;
        private Button _btnPrev;
        private Button _btnNext;
        private Label _lblPageNum;

        private int _currentPage = 1;
        private int _totalPages = 1;
        private int _totalItems = 0;
        private int _pageSize = DEFAULT_PAGE_SIZE;

        public event Action<int> PageChanged;

        public int CurrentPage => _currentPage;
        public int PageSize => _pageSize;
        public int TotalPages => _totalPages;

        public PaginationHelper(int pageSize = DEFAULT_PAGE_SIZE)
        {
            _pageSize = pageSize;
        }

        /// <summary>
        /// Creates the pagination panel with controls
        /// </summary>
        public Panel CreatePaginationPanel(int width = 400)
        {
            _paginationPanel = new Panel();
            _paginationPanel.Size = new Size(width, 40);
            _paginationPanel.BackColor = Color.Transparent;

            // Previous button
            _btnPrev = new Button();
            _btnPrev.Text = "◀ Trước";
            _btnPrev.Size = new Size(80, 30);
            _btnPrev.Location = new Point(0, 5);
            _btnPrev.FlatStyle = FlatStyle.Flat;
            _btnPrev.BackColor = UIHelper.PrimaryColor;
            _btnPrev.ForeColor = Color.White;
            _btnPrev.Font = new Font("Segoe UI", 9);
            _btnPrev.Click += (s, e) => GoToPreviousPage();
            _paginationPanel.Controls.Add(_btnPrev);

            // Page number
            _lblPageNum = new Label();
            _lblPageNum.Text = "1";
            _lblPageNum.Size = new Size(100, 30);
            _lblPageNum.Location = new Point(90, 8);
            _lblPageNum.Font = new Font("Segoe UI Semibold", 11);
            _lblPageNum.TextAlign = ContentAlignment.MiddleCenter;
            _paginationPanel.Controls.Add(_lblPageNum);

            // Next button
            _btnNext = new Button();
            _btnNext.Text = "Sau ▶";
            _btnNext.Size = new Size(80, 30);
            _btnNext.Location = new Point(200, 5);
            _btnNext.FlatStyle = FlatStyle.Flat;
            _btnNext.BackColor = UIHelper.PrimaryColor;
            _btnNext.ForeColor = Color.White;
            _btnNext.Font = new Font("Segoe UI", 9);
            _btnNext.Click += (s, e) => GoToNextPage();
            _paginationPanel.Controls.Add(_btnNext);

            // Info label
            _lblInfo = new Label();
            _lblInfo.Text = "Trang 1/1 (0 mục)";
            _lblInfo.Size = new Size(150, 30);
            _lblInfo.Location = new Point(290, 8);
            _lblInfo.Font = new Font("Segoe UI", 9);
            _lblInfo.ForeColor = Color.Gray;
            _paginationPanel.Controls.Add(_lblInfo);

            UpdateUI();
            return _paginationPanel;
        }

        /// <summary>
        /// Updates pagination with new total count
        /// </summary>
        public void SetTotalItems(int totalItems)
        {
            _totalItems = totalItems;
            _totalPages = Math.Max(1, (int)Math.Ceiling((double)totalItems / _pageSize));
            
            // Reset to page 1 if current page exceeds total
            if (_currentPage > _totalPages)
                _currentPage = 1;

            UpdateUI();
        }

        /// <summary>
        /// Resets to page 1 (for filter changes)
        /// </summary>
        public void ResetToFirstPage()
        {
            _currentPage = 1;
            UpdateUI();
            PageChanged?.Invoke(_currentPage);
        }

        /// <summary>
        /// Go to specific page
        /// </summary>
        public void GoToPage(int page)
        {
            if (page >= 1 && page <= _totalPages)
            {
                _currentPage = page;
                UpdateUI();
                PageChanged?.Invoke(_currentPage);
            }
        }

        private void GoToPreviousPage()
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateUI();
                PageChanged?.Invoke(_currentPage);
            }
        }

        private void GoToNextPage()
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                UpdateUI();
                PageChanged?.Invoke(_currentPage);
            }
        }

        private void UpdateUI()
        {
            if (_lblPageNum != null)
                _lblPageNum.Text = $"{_currentPage} / {_totalPages}";

            if (_lblInfo != null)
                _lblInfo.Text = $"Tổng: {_totalItems} mục";

            if (_btnPrev != null)
                _btnPrev.Enabled = _currentPage > 1;

            if (_btnNext != null)
                _btnNext.Enabled = _currentPage < _totalPages;
        }

        /// <summary>
        /// Calculate skip count for database query
        /// </summary>
        public int GetSkipCount()
        {
            return (_currentPage - 1) * _pageSize;
        }
    }
}
