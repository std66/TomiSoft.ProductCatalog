using System;

namespace TomiSoft.ProductCatalog.BusinessModels.OperationResult {
    public abstract class ResultBM<TResult, TExplanation>
        where TExplanation : struct
        where TResult : class {
        private TExplanation explanation;
        private TResult obj;

        private ResultBM(bool successful) {
            Successful = successful;
        }

        public ResultBM(TExplanation explanation) : this(false) {
            this.explanation = explanation;
        }

        public ResultBM(TResult result) : this(true) {
            this.obj = result ?? throw new ArgumentNullException(nameof(result));
        }

        public bool Successful { get; }

        public TExplanation Explanation {
            get {
                if (Successful)
                    throw new InvalidOperationException($"{nameof(Explanation)} is not available for successful operations.");

                return explanation; 
            }
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
