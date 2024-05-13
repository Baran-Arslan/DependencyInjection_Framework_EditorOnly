namespace iCare.Core.Dependency.Runtime.Attributes.Auto {
    public abstract class BaseAutoAttribute : BaseDependencyAttribute {
        readonly string _id;

        protected BaseAutoAttribute(string id = null) {
            _id = id;
        }

        public string GetId() {
            return _id;
        }
    }
}