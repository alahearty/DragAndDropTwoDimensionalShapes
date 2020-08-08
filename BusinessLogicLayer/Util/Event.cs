using Prism.Events;

namespace BusinessLogicLayer.Util
{
    public class StrokeColorEvent : PubSubEvent<string> { }
    public class SaveNotifier : PubSubEvent<bool> { }
    public class ReturnNotifier : PubSubEvent<Model> { }
    public class FillColorEvent : PubSubEvent<string> { }
    public class CriteriaEvent : PubSubEvent<string> { }
    public class ItemsPaneEvent : PubSubEvent<string> { }
    public class SaveEvent : PubSubEvent { }
    public class LoadEvent : PubSubEvent { }
    public class DeleteEvent : PubSubEvent { }
    public class ClearEvent : PubSubEvent { }
    public class ChildrenOfCanvas : PubSubEvent { }
}
