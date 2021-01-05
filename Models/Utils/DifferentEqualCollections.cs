using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Utils
{
    public class DifferentEqualCollections
    {
    }

    public class GenericCompare<T> : IEqualityComparer<T> where T : class
    {
        private Func<T, object> expr { get; set; }
        public GenericCompare(Func<T, object> expr) => this.expr = expr;

        public bool Equals(T x, T y)
        {
            var first = expr.Invoke(x);
            var sec = expr.Invoke(y);
            if (first != null && first.Equals(sec))
                return true;
            else
                return false;
        }
        public int GetHashCode(T obj)
        {
            return expr.Invoke(obj).GetHashCode();
        }
    }
}
