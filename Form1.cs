using System;
using System.Windows.Forms;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace appforsystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // No action needed when the text in textBox1 changes
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new ScriptEngine
            var engine = Python.CreateEngine();

            // Create a scope and execute the Python code within it
            var scope = engine.CreateScope();
            engine.Execute(@"
import platform

class SystemInfo:
    def __init__(self):
        self.info = {}

    def get_system_info(self):
        self.info['System'] = platform.system()
        self.info['Node Name'] = platform.node()
        self.info['Release'] = platform.release()
        self.info['Version'] = platform.version()
        self.info['Machine'] = platform.machine()
        self.info['Processor'] = platform.processor()

    def print_system_info(self):
        system_info_str = ''
        for key, value in self.info.items():
            system_info_str += f'{key}: {value}\\n'
        return system_info_str

sys_info = SystemInfo()
sys_info.get_system_info()
system_info = sys_info.print_system_info()
", scope);

            // Retrieve the system information from Python and display it
            dynamic systemInfo = scope.GetVariable("system_info");
            MessageBox.Show(systemInfo, "System Information");
        }
    }
}
