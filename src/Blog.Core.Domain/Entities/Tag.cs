using Blog.Core.Domain.Extensions;

namespace Blog.Core.Domain.Entities
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