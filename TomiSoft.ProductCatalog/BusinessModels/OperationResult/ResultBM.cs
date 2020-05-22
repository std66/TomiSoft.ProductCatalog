using System;

namespace TomiSoft.ProductCatalog.BusinessModels.OperationResult {
    public abstract class ResultBM<TResult, TExplanation> : EmptyResultBM<TExplanation>
        where TExplanation : struct
        where TResult : class {
        
        private TResult obj;

        public ResultBM(TExplanation explanation) : base(explanation) {
        }

        public ResultBM(TResult result) : base() {
            this.obj = result ?? throw new ArgumentNullException(nameof(result));
        }

        public TResult Object {
            get {
                if (!Successful)
                    throw new InvalidOperationException($"Result object is not accessible for failed operation. See {nameof(Explanation)} for reasons why the operation has failed.");

                return obj;
            }
        }
    }
}
