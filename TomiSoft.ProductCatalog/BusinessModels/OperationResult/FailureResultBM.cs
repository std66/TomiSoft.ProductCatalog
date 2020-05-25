namespace TomiSoft.ProductCatalog.BusinessModels.OperationResult {
    public class FailureResultBM<TResult, TExplanation> : ResultBM<TResult, TExplanation>
        where TResult : class
        where TExplanation : struct {
        public FailureResultBM(TExplanation explanation) : base(explanation) {
        }
    }
}
