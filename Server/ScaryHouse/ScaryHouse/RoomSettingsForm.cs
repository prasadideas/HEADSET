using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ScaryHouse
{
    public class RoomSettingsForm : Form
    {
        private RoomConfig config;
        private List<NumericUpDown> inputs = new List<NumericUpDown>();
        private List<ComboBox> digitalSelectors = new List<ComboBox>();
        private Button btnSave;
        private Button btnSetAll;
        private NumericUpDown nudSetAll;
        private ComboBox cbNumRooms;
        private ComboBox cbNumDigitalScreens;
        private FlowLayoutPanel panel;

        public RoomSettingsForm(RoomConfig cfg)
        {
            this.config = cfg;
            Initialize();
        }

        private void Initialize()
        {
            this.Text = "Room Timers";
            this.Size = new Size(420, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.FlowDirection = FlowDirection.TopDown;
            panel.Padding = new Padding(10);
            panel.AutoScroll = true;
            this.Controls.Add(panel);

            // number of rooms selector
            cbNumRooms = new ComboBox();
            cbNumRooms.Width = 80; cbNumRooms.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 1; i <= 20; i++) cbNumRooms.Items.Add(i.ToString());
            cbNumRooms.SelectedIndexChanged += CbNumRooms_SelectedIndexChanged;
            // safe set initial selection
            if (cbNumRooms.Items.Count > 0)
            {
                var roomsStr = config.NumberOfRooms.ToString();
                if (cbNumRooms.Items.Contains(roomsStr)) cbNumRooms.SelectedItem = roomsStr; else cbNumRooms.SelectedIndex = 0;
            }

            // number of digital screens selector
            cbNumDigitalScreens = new ComboBox();
            cbNumDigitalScreens.Width = 80; cbNumDigitalScreens.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 1; i <= 20; i++) cbNumDigitalScreens.Items.Add(i.ToString());
            cbNumDigitalScreens.SelectedIndexChanged += CbNumDigitalScreens_SelectedIndexChanged;
            if (cbNumDigitalScreens.Items.Count > 0)
            {
                var dsStr = config.NumberOfDigitalScreens.ToString();
                if (cbNumDigitalScreens.Items.Contains(dsStr)) cbNumDigitalScreens.SelectedItem = dsStr; else cbNumDigitalScreens.SelectedIndex = 0;
            }

            // build initial inputs (this will re-add the selectors into the panel)
            BuildRoomInputs(config.NumberOfRooms);

            var bottom = new Panel();
            bottom.Height = 60; bottom.Dock = DockStyle.Bottom;

            btnSave = new Button() { Text = "Save", Width = 90, Left = 200, Top = 12 };
            btnSave.Click += BtnSave_Click;

            btnSetAll = new Button() { Text = "Set All", Width = 90, Left = 20, Top = 12 };
            btnSetAll.Click += BtnSetAll_Click;

            nudSetAll = new NumericUpDown() { Left = 120, Top = 14, Width = 60, Minimum = 1, Maximum = 3600, Value = 20 };

            bottom.Controls.Add(btnSetAll);
            bottom.Controls.Add(nudSetAll);
            bottom.Controls.Add(btnSave);
            this.Controls.Add(bottom);
        }

        private void CbNumRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cbNumRooms.SelectedItem as string, out int v))
            {
                BuildRoomInputs(v);
            }
        }

        private void CbNumDigitalScreens_SelectedIndexChanged(object sender, EventArgs e)
        {
            // rebuild inputs to reflect digital screen count
            int roomCount = config.NumberOfRooms;
            if (int.TryParse(cbNumRooms.SelectedItem as string, out int sel)) roomCount = sel;
            BuildRoomInputs(roomCount);
        }

        private void BuildRoomInputs(int count)
        {
            // defensive: ensure combo boxes exist
            if (cbNumRooms == null)
            {
                cbNumRooms = new ComboBox();
                for (int i = 1; i <= 20; i++) cbNumRooms.Items.Add(i.ToString());
                cbNumRooms.DropDownStyle = ComboBoxStyle.DropDownList;
                cbNumRooms.SelectedIndexChanged += CbNumRooms_SelectedIndexChanged;
            }
            if (cbNumDigitalScreens == null)
            {
                cbNumDigitalScreens = new ComboBox();
                for (int i = 1; i <= 20; i++) cbNumDigitalScreens.Items.Add(i.ToString());
                cbNumDigitalScreens.DropDownStyle = ComboBoxStyle.DropDownList;
                cbNumDigitalScreens.SelectedIndexChanged += CbNumDigitalScreens_SelectedIndexChanged;
            }

            // remove any existing room entries
            panel.Controls.Clear();

            // re-add top selector panel
            var topPanel = new Panel();
            topPanel.Width = 360; topPanel.Height = 72;
            var lblCount = new Label();
            lblCount.Text = "Number of rooms:";
            lblCount.Left = 4; lblCount.Top = 8; lblCount.Width = 120;

            cbNumRooms.Left = 130; cbNumRooms.Top = 4; cbNumRooms.Width = 80;

            var lblDigits = new Label();
            lblDigits.Text = "Digital screens:";
            lblDigits.Left = 4; lblDigits.Top = 44; lblDigits.Width = 120;

            cbNumDigitalScreens.Left = 130; cbNumDigitalScreens.Top = 40; cbNumDigitalScreens.Width = 80;

            topPanel.Controls.Add(lblCount);
            topPanel.Controls.Add(cbNumRooms);
            topPanel.Controls.Add(lblDigits);
            topPanel.Controls.Add(cbNumDigitalScreens);
            panel.Controls.Add(topPanel);

            inputs.Clear();
            digitalSelectors.Clear();

            // ensure config.RoomSeconds large enough
            if (config.RoomSeconds == null) config.RoomSeconds = new List<int>();
            while (config.RoomSeconds.Count < count) config.RoomSeconds.Add(20);
            if (config.RoomSeconds.Count > 20) config.RoomSeconds = config.RoomSeconds.GetRange(0, 20);

            // ensure digital screens list large enough and validate count
            int digitalCount = config.NumberOfDigitalScreens;
            if (int.TryParse(cbNumDigitalScreens.SelectedItem as string, out int ds)) digitalCount = ds;
            if (digitalCount < 1) digitalCount = 1;
            if (digitalCount > 20) digitalCount = 20;

            if (config.DigitalScreenRoom == null) config.DigitalScreenRoom = new List<int>();
            while (config.DigitalScreenRoom.Count < digitalCount) config.DigitalScreenRoom.Add(1);
            if (config.DigitalScreenRoom.Count > 20) config.DigitalScreenRoom = config.DigitalScreenRoom.GetRange(0, 20);
            if (config.DigitalScreenRoom.Count > digitalCount) config.DigitalScreenRoom = config.DigitalScreenRoom.GetRange(0, digitalCount);

            for (int i = 0; i < count; i++)
            {
                var p = new Panel();
                p.Width = 360; p.Height = 36;

                var lbl = new Label();
                lbl.Text = $"Room {i + 1}";
                lbl.Left = 4; lbl.Top = 8; lbl.Width = 120;

                var nud = new NumericUpDown();
                nud.Left = 130; nud.Top = 4; nud.Width = 80;
                nud.Minimum = 1; nud.Maximum = 3600;
                nud.Value = config.RoomSeconds.Count > i ? config.RoomSeconds[i] : 20;

                p.Controls.Add(lbl);
                p.Controls.Add(nud);
                panel.Controls.Add(p);

                inputs.Add(nud);
            }

            // add header for digital screens mappings
            var hdr = new Label();
            hdr.Text = "Digital screen mappings:";
            hdr.AutoSize = true;
            hdr.Margin = new Padding(4, 8, 4, 4);
            panel.Controls.Add(hdr);

            for (int i = 0; i < digitalCount; i++)
            {
                var p = new Panel();
                p.Width = 360; p.Height = 36;

                var lbl = new Label();
                lbl.Text = $"Screen {i + 1}";
                lbl.Left = 4; lbl.Top = 8; lbl.Width = 120;

                var cb = new ComboBox();
                cb.Left = 130; cb.Top = 4; cb.Width = 120; cb.DropDownStyle = ComboBoxStyle.DropDownList;
                // populate rooms
                cb.Items.Clear();
                for (int r = 1; r <= count; r++) cb.Items.Add(r.ToString());
                int mapped = (config.DigitalScreenRoom.Count > i) ? config.DigitalScreenRoom[i] : 1;
                if (mapped < 1) mapped = 1;
                if (mapped > count) mapped = count;
                var mappedStr = mapped.ToString();
                if (cb.Items.Contains(mappedStr)) cb.SelectedItem = mappedStr; else if (cb.Items.Count > 0) cb.SelectedIndex = 0;

                p.Controls.Add(lbl);
                p.Controls.Add(cb);
                panel.Controls.Add(p);

                digitalSelectors.Add(cb);
            }

            // update config's number of rooms/digital screens to reflect current selections
            config.NumberOfRooms = count;
            config.NumberOfDigitalScreens = digitalCount;
        }

        private void BtnSetAll_Click(object sender, EventArgs e)
        {
            int v = (int)nudSetAll.Value;
            foreach (var n in inputs) n.Value = v;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = config.NumberOfRooms;
            if (int.TryParse(cbNumRooms.SelectedItem as string, out int sel)) count = sel;
            config.NumberOfRooms = count;

            int digitalCount = config.NumberOfDigitalScreens;
            if (int.TryParse(cbNumDigitalScreens.SelectedItem as string, out int ds)) digitalCount = ds;
            config.NumberOfDigitalScreens = digitalCount;

            // ensure list length
            if (config.RoomSeconds == null) config.RoomSeconds = new List<int>();
            while (config.RoomSeconds.Count < count) config.RoomSeconds.Add(20);
            if (config.RoomSeconds.Count > count) config.RoomSeconds = config.RoomSeconds.GetRange(0, count);

            for (int i = 0; i < count; i++)
            {
                config.RoomSeconds[i] = (int)inputs[i].Value;
            }

            // ensure digital mapping list length
            if (config.DigitalScreenRoom == null) config.DigitalScreenRoom = new List<int>();
            while (config.DigitalScreenRoom.Count < digitalCount) config.DigitalScreenRoom.Add(1);
            if (config.DigitalScreenRoom.Count > digitalCount) config.DigitalScreenRoom = config.DigitalScreenRoom.GetRange(0, digitalCount);

            for (int i = 0; i < digitalCount; i++)
            {
                int mapped = 1;
                if (i < digitalSelectors.Count)
                {
                    if (int.TryParse(digitalSelectors[i].SelectedItem as string, out int mv)) mapped = mv;
                }
                // clamp
                if (mapped < 1) mapped = 1;
                if (mapped > config.NumberOfRooms) mapped = config.NumberOfRooms;
                config.DigitalScreenRoom[i] = mapped;
            }

            config.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
