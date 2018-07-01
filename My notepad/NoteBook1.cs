using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_notepad
{
    public partial class NoteBook : Form
    {
        public bool issave;
        public bool iswrite;
        public string open;
        private FontDialog fn = new FontDialog();
        public NoteBook()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newfunc();
             
        }

        private void newfunc()
        {
            if (iswrite)
            {
                DialogResult rslt = MessageBox.Show("Do You want to save changes?", "Save File",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                switch (rslt)
                {
                    case DialogResult.Yes:
                        save();
                        break;
                    case DialogResult.No:
                        clearscreen();
                        break;
                }

            }
            else
            {
                clearscreen();
                undoredoenabledisable(false);
            }
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openfunc();  


        }

        private void openfunc()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Text document (*.txt*)|*.txt|All Files(*.*)|*.*";
            DialogResult rslt = op.ShowDialog();
            if (rslt == DialogResult.OK)
            {
                if (Path.GetExtension(op.FileName) == ".txt")
                    TextBox.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                this.Text = Path.GetFileName(op.FileName) + " -NoteBook";
                issave = true;
                iswrite = false;
                open = op.FileName;
                undoredoenabledisable(false);


            }

        }
        private void undoredoenabledisable( bool enable)
        {
            undoToolStripMenuItem.Enabled = enable;
            redoToolStripMenuItem.Enabled = enable;
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();

        }

        private void save()
        {
            if (issave)
            {
                if (Path.GetExtension(open) == ".txt")
                    TextBox.SaveFile(open, RichTextBoxStreamType.PlainText);
                iswrite = false;
            }
            else
            {
                if (iswrite)
                {
                    saveas();
                }
                else
                {
                    clearscreen();
                }

            }
        }

        private void clearscreen()
        {
            TextBox.Clear();
            this.Text = "NoteBook";
            iswrite = false;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveas();
        }

        private void saveas()
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text document(*.txt)|*.txt|All Files(*.*)|*.*";
            DialogResult rslt = sf.ShowDialog();
            if (rslt == DialogResult.OK)
            {
                if (Path.GetExtension(sf.FileName) == ".txt")
                    TextBox.SaveFile(sf.FileName, RichTextBoxStreamType.PlainText);
                this.Text = Path.GetFileName(sf.FileName) + " -NoteBook";
                issave = true;
                iswrite = false;
                open = sf.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (iswrite)
            {
                newfunc();
                Application.Exit();
            }
            else
            {
                Application.Exit();
            }

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void undo()
        {
            TextBox.Undo();
            redoToolStripMenuItem.Enabled = true;
            toolStripButton6.Enabled = false;
            toolStripButton7.Enabled = true;
           undoToolStripMenuItem.Enabled = false;
            

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redo();
        }

        private void redo()
        {
            TextBox.Redo();
            undoToolStripMenuItem.Enabled = true;
            toolStripButton6.Enabled = true;
            toolStripButton7.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.Paste();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {


            fontfunc();
        }

        private void fontfunc()
        {
            fn.ShowColor = true;
            fn.ShowApply = true;
            fn.Apply += new System.EventHandler(fn_Apply);

            fn.Font = TextBox.SelectionFont;
            if (fn.ShowDialog() == DialogResult.OK)
            {
                if (TextBox.SelectionLength > 0)
                {
                    TextBox.SelectionFont = fn.Font;
                    TextBox.SelectionColor = fn.Color;
                }
            }
        }
        private void fn_Apply(object sender, EventArgs e)
        {
            applyfontcolor();
        }

        private void applyfontcolor()
        {
            if (TextBox.SelectionLength > 0)
            {
                TextBox.SelectionFont = fn.Font;
                TextBox.SelectionColor = fn.Color;
            }
        }
        private void backgroundColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backcolorfunc();
        }

        private void backcolorfunc()
        {
            ColorDialog cl = new ColorDialog();
            if (cl.ShowDialog() == DialogResult.OK)
            {
                TextBox.BackColor = cl.Color;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version 1.0\nMade by two Ubitians Jawad khan & Ubaid Hussain","About NoteBook",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void NoteBook_Load(object sender, EventArgs e)
        {
            issave=false;
            iswrite=false;
            open="";
           
            
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            iswrite = true;
            undoToolStripMenuItem.Enabled = true;
            toolStripButton6.Enabled = true;
            
        }

        private void New_Click(object sender, EventArgs e)
        {
            newfunc();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            openfunc();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            save();
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            saveas();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            redo();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            TextBox.Copy();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            TextBox.Cut();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            TextBox.Paste();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            fontfunc();
            applyfontcolor();

        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            backcolorfunc();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version 1.0\nMade by two Ubitians Jawad khan & Ubaid Hussain", "About NoteBook", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        
    }
}
