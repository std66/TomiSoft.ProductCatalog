using System;

namespace TomiSoft.ProductCatalog.BusinessModels.OperationResult {
    /// <summary>
    /// Represents the result of an operation that can succeed or fail, but it does not provide any additional information when succeeds.
    /// However, it provides an explanation why the operation has failed.
    /// </summary>
    /// <typeparam name="TExplanation">A struct that can provide an explanation in case of a non-successful outcome.</typeparam>
    public class EmptyResultBM<TExplanation>
        where TExplanation : struct {
        private readonly TExplanation explanation;

        /// <summary>
        /// Gets if the operation has succeeded or not.
        /// </summary>
        public bool Successful { get; }

        /// <summary>
        /// Gets an explanation why the operation has failed. Only available if <see cref="Successful"/> is false.
        /// </summary>
        /// <exception cref="InvalidOperationException">when <see cref="Successful"/> is true, meaning that the operation succeeded</exception>
        public TExplanation Explanation {
            get {
                if (Successful)
                    throw new InvalidOperationException($"{nameof(Explanation)} is not available for successful operations.");

                return explanation;
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="EmptyResultBM{TExplanation}"/> that represents the successful outcome of an operation.
        /// </summary>
        public EmptyResultBM() {
            this.Successful = true;
        }

        /// <summary>
        /// Creates a new instance of <see cref="EmptyResultBM{TExplanation}"/> that represents a non-successful outcome of an operation.
        /// </summary>
        /// <param name="explanation">An object that explains why the operation has failed.</param>
        public EmptyResultBM(TExplanation explanation) {
            this.Successful = false;
            this.explanation = explanation;
        }
    }
}
