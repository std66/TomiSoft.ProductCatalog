namespace TomiSoft.ProductCatalog.BusinessModels.OperationResult {
    public class SuccessfulResultBM<TResult, TExplanation> : ResultBM<TResult, TExplanation>
        where TResult : class
        where TExplanation : struct {

        public SuccessfulResultBM(TResult result) : base(result) {
        }
    }
}
