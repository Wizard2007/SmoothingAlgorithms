using System;
using System.Collections;

namespace SmoothingAlgorithm.UnitTests.Comparers
{
    /// <inheritdoc />
    public class CompareDoubleWithTolorance : IComparer
    {
        #region Private

        private double _tolorance;

        #endregion

        #region Constructor

        /// <summary>
        /// Create new instance of <see cref="CompareDoubleWithTolorance"/>
        /// </summary>
        /// <param name="tolorance">Needed tolerance to compare two digits.</param>
        public CompareDoubleWithTolorance(double tolorance = 1e-6)
            => _tolorance = tolorance;

        #endregion

        #region Public methods

        public int Compare(object x, object y)
        {
            if (Math.Abs((double)x - (double)y) < _tolorance)
            {
                return 0;
            }

            if ((double)x < (double)y)
            {
                return -1;
            }

            return 1;
        }

        #endregion
    }
}
