
namespace TravelCompanyView
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonUpdateList = new System.Windows.Forms.Button();
            this.ButtonOrderDelivered = new System.Windows.Forms.Button();
            this.ButtonCreateOrder = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conditionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.travelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRep = new System.Windows.Forms.ToolStripMenuItem();
            this.TravelListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TravelConditionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OrderListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStartWork = new System.Windows.Forms.ToolStripMenuItem();
            this.implementerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonUpdateList
            // 
            this.ButtonUpdateList.Location = new System.Drawing.Point(957, 407);
            this.ButtonUpdateList.Name = "ButtonUpdateList";
            this.ButtonUpdateList.Size = new System.Drawing.Size(283, 43);
            this.ButtonUpdateList.TabIndex = 15;
            this.ButtonUpdateList.Text = "Обновить список";
            this.ButtonUpdateList.UseVisualStyleBackColor = true;
            this.ButtonUpdateList.Click += new System.EventHandler(this.ButtonUpdateList_Click);
            // 
            // ButtonOrderDelivered
            // 
            this.ButtonOrderDelivered.Location = new System.Drawing.Point(957, 326);
            this.ButtonOrderDelivered.Name = "ButtonOrderDelivered";
            this.ButtonOrderDelivered.Size = new System.Drawing.Size(283, 43);
            this.ButtonOrderDelivered.TabIndex = 14;
            this.ButtonOrderDelivered.Text = "Заказ выдан";
            this.ButtonOrderDelivered.UseVisualStyleBackColor = true;
            this.ButtonOrderDelivered.Click += new System.EventHandler(this.ButtonOrderDelivered_Click);
            // 
            // ButtonCreateOrder
            // 
            this.ButtonCreateOrder.Location = new System.Drawing.Point(957, 245);
            this.ButtonCreateOrder.Name = "ButtonCreateOrder";
            this.ButtonCreateOrder.Size = new System.Drawing.Size(283, 43);
            this.ButtonCreateOrder.TabIndex = 11;
            this.ButtonCreateOrder.Text = "Создать заказ";
            this.ButtonCreateOrder.UseVisualStyleBackColor = true;
            this.ButtonCreateOrder.Click += new System.EventHandler(this.ButtonCreateOrder_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 31);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 29;
            this.dataGridView.Size = new System.Drawing.Size(936, 622);
            this.dataGridView.TabIndex = 10;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem,
            this.toolStripMenuItemRep,
            this.toolStripMenuItemStartWork});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1252, 28);
            this.menuStrip.TabIndex = 16;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conditionToolStripMenuItem,
            this.travelToolStripMenuItem,
            this.clientToolStripMenuItem,
            this.implementerToolStripMenuItem});
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(117, 24);
            this.toolStripMenuItem.Text = "Справочники";
            // 
            // conditionToolStripMenuItem
            // 
            this.conditionToolStripMenuItem.Name = "conditionToolStripMenuItem";
            this.conditionToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.conditionToolStripMenuItem.Text = "Условия";
            // 
            // travelToolStripMenuItem
            // 
            this.travelToolStripMenuItem.Name = "travelToolStripMenuItem";
            this.travelToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.travelToolStripMenuItem.Text = "Туристические путевки";
            // 
            // clientToolStripMenuItem
            // 
            this.clientToolStripMenuItem.Name = "clientToolStripMenuItem";
            this.clientToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.clientToolStripMenuItem.Text = "Клиенты";
            // 
            // toolStripMenuItemRep
            // 
            this.toolStripMenuItemRep.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TravelListToolStripMenuItem,
            this.TravelConditionsToolStripMenuItem,
            this.OrderListToolStripMenuItem});
            this.toolStripMenuItemRep.Name = "toolStripMenuItemRep";
            this.toolStripMenuItemRep.Size = new System.Drawing.Size(73, 24);
            this.toolStripMenuItemRep.Text = "Отчеты";
            // 
            // TravelListToolStripMenuItem
            // 
            this.TravelListToolStripMenuItem.Name = "TravelListToolStripMenuItem";
            this.TravelListToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.TravelListToolStripMenuItem.Text = "Список путевок";
            // 
            // TravelConditionsToolStripMenuItem
            // 
            this.TravelConditionsToolStripMenuItem.Name = "TravelConditionsToolStripMenuItem";
            this.TravelConditionsToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.TravelConditionsToolStripMenuItem.Text = "Условия по путевкам";
            // 
            // OrderListToolStripMenuItem
            // 
            this.OrderListToolStripMenuItem.Name = "OrderListToolStripMenuItem";
            this.OrderListToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.OrderListToolStripMenuItem.Text = "Список заказов";
            // 
            // toolStripMenuItemStartWork
            // 
            this.toolStripMenuItemStartWork.Name = "toolStripMenuItemStartWork";
            this.toolStripMenuItemStartWork.Size = new System.Drawing.Size(125, 24);
            this.toolStripMenuItemStartWork.Text = "Запуск работы";
            // 
            // implementerToolStripMenuItem
            // 
            this.implementerToolStripMenuItem.Name = "implementerToolStripMenuItem";
            this.implementerToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.implementerToolStripMenuItem.Text = "Исполнители";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 655);
            this.Controls.Add(this.ButtonUpdateList);
            this.Controls.Add(this.ButtonOrderDelivered);
            this.Controls.Add(this.ButtonCreateOrder);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip);
            this.Name = "FormMain";
            this.Text = "Туристическая фирма";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonUpdateList;
        private System.Windows.Forms.Button ButtonOrderDelivered;
        private System.Windows.Forms.Button ButtonCreateOrder;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conditionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem travelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRep;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTravelCond;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOrders;
        private System.Windows.Forms.ToolStripMenuItem conditionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TravelConditionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TravelConditionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TravelListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OrderListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStartWork;
        private System.Windows.Forms.ToolStripMenuItem implementerToolStripMenuItem;
    }
}

