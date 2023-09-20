namespace isj23 {
    public interface ITimeAffected {
        void AttachTimeEvents();
        void DetachTimeEvents();
        void OnPlayTimeStarts();
        void OnPlayTimeStops();
        void OnPlayTimeRestore();
    } 
}