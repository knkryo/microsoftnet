
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

    /// <summary>
    /// ValidationConnection
    /// </summary>
	public class ValidationCollection : System.Collections.Generic.List<IValidation>
    {

        #region Delegate Declare

        public event ValidatingEventHandler Validating;
		public delegate void ValidatingEventHandler(object sender, ValidatingEventArgs e);

		public event ValidatedEventHandler Validated;
		public delegate void ValidatedEventHandler(object sender, ValidatingEventArgs e);

		public event RaiseInvalidValidationEventHandler RaiseInvalidValidation;

		public delegate void RaiseInvalidValidationEventHandler(object sender, ValidatingEventArgs e);

        #endregion

        #region Event
        
        protected virtual void OnValidating(ValidatingEventArgs e)
		{
			if (Validating != null) {
				Validating(this, e);
			}
		}

		protected virtual void OnValidated(ValidatingEventArgs e)
		{
			if (Validated != null) {
				Validated(this, e);
			}
		}


		protected virtual void OnRaiseInvalidValidation(ValidatingEventArgs e)
		{
            if (RaiseInvalidValidation != null)
            {
                RaiseInvalidValidation(this, e);
            }
		}

        #endregion

        public bool IsBreakValidError{get;set;}

        #region Method
        
        public bool Valid()
		{

			bool bResult = true;
			ValidatingEventArgs validEvent = null;

			foreach (IValidation Validation in this) {
				validEvent = new ValidatingEventArgs(Validation);

				this.OnValidating(validEvent);


				if (Validation.IsValid() == false) {
					this.OnRaiseInvalidValidation(validEvent);

					bResult = false;

                    if (this.IsBreakValidError == true)
                    {
						//継続しない
						break;
					}

				}

				this.OnValidated(validEvent);

			}

			return bResult;

        }

        #endregion

    }

}
