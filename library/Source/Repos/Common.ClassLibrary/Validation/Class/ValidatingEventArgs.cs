using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ClassLibrary.Validation
{
    public class ValidatingEventArgs : System.EventArgs
    {


        private IValidation _Validatin;
        public ValidatingEventArgs(IValidation validation)
        {
            this._Validatin = validation;
        }

        public IValidation Validation
        {
            get { return _Validatin; }
            set { _Validatin = value; }
        }


    }
}
