namespace Trumpf.Hmi.Uif.List.Dependencies.Services
{
    public class ListOptions
    {
        public ListOptions(bool outdated, bool minor, bool patch, bool includePrereleases)
        {
            Outdated = outdated;
            Minor = minor;
            Patch = patch;
            IncludePrereleases = includePrereleases;
        }

        public bool Outdated { get; }

        public bool Minor { get; }

        public bool Patch { get; }

        public bool IncludePrereleases { get; }
    }
}
