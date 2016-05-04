using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
namespace Common.WinForms.CustomComponents
{
    public partial class ToggleButton
    {

        private ToggleTypeAttribute _ToggleType = ToggleTypeAttribute.On;
        public enum ToggleTypeAttribute
        {
            On = 1,
            Off = 2
        }

        #region Public Property

        #region OnModeText

        /// <summary>
        /// トグルの状態 = Onの場合のテキスト値を設定します
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [System.ComponentModel.Description("トグルの状態 = Onの場合のテキスト値を設定します")]
        [System.ComponentModel.DefaultValueAttribute("")]
        public string OnModeText{get;set;}

        #endregion

        #region OffModeText

        /// <summary>
        /// トグルの状態 = Offの場合のテキスト値を設定します
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [System.ComponentModel.Description("トグルの状態 = Offの場合のテキスト値を設定します")]
        [System.ComponentModel.DefaultValueAttribute("")]
        public string OffModeText { get; set; }

        #endregion

        #region ToggleStatus

        /// <summary>
        /// トグルの状態を設定します
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [System.ComponentModel.Description("トグルの状態を設定します")]
        public ToggleTypeAttribute ToggleStatus
        {
            get { return _ToggleType; }
            set
            {
                _ToggleType = value;
                if (_ToggleType == ToggleTypeAttribute.On)
                {
                    this.Text = OnModeText;
                }
                else
                {
                    this.Text = OffModeText;
                }
            }
        }

        private bool ShouldSerializeToggleType()
        {
            return !(this.ToggleStatus == ToggleTypeAttribute.On);
        }

        #endregion

        #endregion

        #region Events

        protected override void OnClick(EventArgs e)
        {
            if (this.ToggleStatus == ToggleTypeAttribute.On)
            {
                this.ToggleStatus = ToggleTypeAttribute.Off;
            }
            else
            {
                this.ToggleStatus = ToggleTypeAttribute.On;
            }
            this.Refresh();
            base.OnClick(e);
        }

        #endregion

    }
}
