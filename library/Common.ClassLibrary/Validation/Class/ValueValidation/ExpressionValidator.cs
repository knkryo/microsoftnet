using System;

namespace Common.ClassLibrary.Validation
{
    /// <summary>
    /// 検証が正しいかどうかをチェックするValidator
    /// </summary>
    public class ExpressionValidator : AbstructValidation
    {
        private bool _isSuccess = false;

        public ExpressionValidator(bool successExpression)
        {
            _isSuccess = successExpression;
        }

        #region Method

        #region CheckValidator

        /// <summary>
        /// 検証済みの結果を返す
        /// </summary>
        /// <returns></returns>
        public override bool IsValid()
        {

            return _isSuccess;
        }

        #endregion

        #endregion

    }
}
