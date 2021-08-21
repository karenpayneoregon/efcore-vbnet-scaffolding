<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.CustomersDataGridView = New System.Windows.Forms.DataGridView()
        Me.CompanyNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StreetColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CityColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChangesTextBox = New System.Windows.Forms.TextBox()
        Me.AddButton = New System.Windows.Forms.Button()
        Me.ShowChangesButton = New System.Windows.Forms.Button()
        Me.SaveChangesCheckBox = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.CustomersDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CustomersDataGridView
        '
        Me.CustomersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CustomersDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CompanyNameColumn, Me.StreetColumn, Me.CityColumn})
        Me.CustomersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CustomersDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.CustomersDataGridView.Name = "CustomersDataGridView"
        Me.CustomersDataGridView.RowTemplate.Height = 25
        Me.CustomersDataGridView.Size = New System.Drawing.Size(604, 416)
        Me.CustomersDataGridView.TabIndex = 6
        '
        'CompanyNameColumn
        '
        Me.CompanyNameColumn.DataPropertyName = "CompanyName"
        Me.CompanyNameColumn.HeaderText = "Company"
        Me.CompanyNameColumn.Name = "CompanyNameColumn"
        '
        'StreetColumn
        '
        Me.StreetColumn.DataPropertyName = "Street"
        Me.StreetColumn.HeaderText = "Street"
        Me.StreetColumn.Name = "StreetColumn"
        '
        'CityColumn
        '
        Me.CityColumn.DataPropertyName = "City"
        Me.CityColumn.HeaderText = "City"
        Me.CityColumn.Name = "CityColumn"
        '
        'ChangesTextBox
        '
        Me.ChangesTextBox.Location = New System.Drawing.Point(8, 34)
        Me.ChangesTextBox.Multiline = True
        Me.ChangesTextBox.Name = "ChangesTextBox"
        Me.ChangesTextBox.PlaceholderText = "No changes detected"
        Me.ChangesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ChangesTextBox.Size = New System.Drawing.Size(581, 68)
        Me.ChangesTextBox.TabIndex = 3
        '
        'AddButton
        '
        Me.AddButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddButton.Enabled = False
        Me.AddButton.Location = New System.Drawing.Point(487, 3)
        Me.AddButton.Name = "AddButton"
        Me.AddButton.Size = New System.Drawing.Size(112, 23)
        Me.AddButton.TabIndex = 4
        Me.AddButton.Text = "Mock Add"
        Me.AddButton.UseVisualStyleBackColor = True
        '
        'ShowChangesButton
        '
        Me.ShowChangesButton.Enabled = False
        Me.ShowChangesButton.Location = New System.Drawing.Point(10, 3)
        Me.ShowChangesButton.Name = "ShowChangesButton"
        Me.ShowChangesButton.Size = New System.Drawing.Size(144, 23)
        Me.ShowChangesButton.TabIndex = 1
        Me.ShowChangesButton.Text = "Show changes"
        Me.ShowChangesButton.UseVisualStyleBackColor = True
        '
        'SaveChangesCheckBox
        '
        Me.SaveChangesCheckBox.AutoSize = True
        Me.SaveChangesCheckBox.Location = New System.Drawing.Point(161, 7)
        Me.SaveChangesCheckBox.Name = "SaveChangesCheckBox"
        Me.SaveChangesCheckBox.Size = New System.Drawing.Size(99, 19)
        Me.SaveChangesCheckBox.TabIndex = 2
        Me.SaveChangesCheckBox.Text = "Save Changes"
        Me.SaveChangesCheckBox.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ChangesTextBox)
        Me.Panel1.Controls.Add(Me.AddButton)
        Me.Panel1.Controls.Add(Me.ShowChangesButton)
        Me.Panel1.Controls.Add(Me.SaveChangesCheckBox)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 416)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(604, 112)
        Me.Panel1.TabIndex = 7
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 528)
        Me.Controls.Add(Me.CustomersDataGridView)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.CustomersDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CustomersDataGridView As DataGridView
    Friend WithEvents CompanyNameColumn As DataGridViewTextBoxColumn
    Friend WithEvents StreetColumn As DataGridViewTextBoxColumn
    Friend WithEvents CityColumn As DataGridViewTextBoxColumn
    Friend WithEvents ChangesTextBox As TextBox
    Friend WithEvents AddButton As Button
    Friend WithEvents ShowChangesButton As Button
    Friend WithEvents SaveChangesCheckBox As CheckBox
    Friend WithEvents Panel1 As Panel
End Class
