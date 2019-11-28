﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            lblLastName.Text = Resource1.FullName;
            btnAdd.Text = Resource1.Add;
            btnWrite.Text = Resource1.Write;
            btnDelete.Text = Resource1.Delete;

            Listázás();

        }

        private void Listázás()
        {
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User();
            u.FullName = tbxFullName.Text;
            users.Add(u);

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Comma Seperated Values (*.csv)|*.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() !=DialogResult.OK)
            {
                return;
            }

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var item in users)
                {
                    sw.Write(item.ID);
                    sw.Write(";");
                    sw.Write(item.FullName);
                    sw.WriteLine();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == null) return;
            User törlendő = (User)listBox1.SelectedItem;
            users.Remove(törlendő);
            Listázás();

        }
    }
}
