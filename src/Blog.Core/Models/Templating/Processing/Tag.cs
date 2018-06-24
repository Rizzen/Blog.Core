using Blog.Core.Extensions;

namespace Blog.Core.Models.Templating.Processing
{
    public class Tag
    {
        public string Name { get; }
        public string ResolutionName { get; }

        public Tag(string name)
        {
            Name = name;
            ResolutionName = name.ToUnderscoreCase();
        }

        public override string ToString() => Name;

        public override int GetHashCode() => Name.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is Tag t)
                return t.Name == Name;

            return false;
        }
    }
}