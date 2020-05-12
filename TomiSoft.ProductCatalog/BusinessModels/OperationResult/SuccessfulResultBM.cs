namespace TomiSoft.ProductCatalog.BusinessModels.OperationResult {
    internal class SuccessfulResultBM<TResult, TExplanation> : ResultBM<TResult, TExplanation>
        where TResult : class
        where TExplanation : struct {

        public SuccessfulResultBM(TResult result) : base(result) {
        }
    }
}
