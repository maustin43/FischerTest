using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FischerDataLayer.DataObjects;

namespace DataLayerTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			ActivityBase activity = new ActivityBase(1);
		}
	}
}
