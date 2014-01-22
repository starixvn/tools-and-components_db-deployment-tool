namespace DbDeployer.UserControls
{
    using System.Drawing;
    using System.Windows.Forms;

    public class SMSColumnHeader : DataGridViewColumnHeaderCell
    {
        private Rectangle CheckBoxRegion;
        private bool checkAll = false;

        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates dataGridViewElementState,
            object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value,
                formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            graphics.FillRectangle(new SolidBrush(cellStyle.BackColor), cellBounds);

            this.CheckBoxRegion = new Rectangle(
                cellBounds.Location.X + 1,
                cellBounds.Location.Y + 2,
                25, cellBounds.Size.Height - 4);


            if (this.checkAll)
                ControlPaint.DrawCheckBox(graphics, this.CheckBoxRegion, ButtonState.Checked);
            else
                ControlPaint.DrawCheckBox(graphics, this.CheckBoxRegion, ButtonState.Normal);

            Rectangle normalRegion =
                new Rectangle(
                cellBounds.Location.X + 1 + 25,
                cellBounds.Location.Y + 3,
                cellBounds.Size.Width - 30,
                cellBounds.Size.Height);

            graphics.DrawString(value.ToString(), cellStyle.Font, new SolidBrush(cellStyle.ForeColor), normalRegion);
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            //Convert the CheckBoxRegion 
            Rectangle rec = new Rectangle(new Point(0, 0), this.CheckBoxRegion.Size);
            this.checkAll = !this.checkAll;
            if (rec.Contains(e.Location))
            {
                this.DataGridView.Invalidate();
            }
            base.OnMouseClick(e);
        }

        public bool CheckAll
        {
            get { return this.checkAll; }
            set { this.checkAll = value; }
        }

    }
}
