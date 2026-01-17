using System;

public enum RiskLevel
    {
        Low,
        Medium,
        High,
        Critical
    }

public enum SDLCStage
    {
        Backlog = 0,
        Requirement = 0,
        Design = 0,
        Development = 1,
        CodeReview = 1,
        Testing = 1,
        UAT = 1,
        Deployment = 0,
        Maintenance = 2
    }

public sealed class Requirement
    {
        public int Id{get;}
        public string Title{get;}
        public RiskLevel Risk{get;}

        public Requirement(int id, string title, RiskLevel risk)
        {
            Id = id;
            Title = title;
            Risk = risk;
        }
    }
public sealed class Workitem
{
    public int Id{get;}
    public string Name{get;}
    public SDLCStage Stage{get;set;}
    public HashSet<int> DependencyIds{get;}

    public Workitem(int id, string name , SDLCStage stage)
    {
        Id = id;
        Name = name;
        Stage = stage;
        DependencyIds = new HashSet<int>();
    }
}

public  sealed class BuildSnapshot
{
    public string Version{get;}
    public DateTime Timestamp{get;}
    public BuildSnapshot(string version)
    {
        Version = version;
        Timestamp = DateTime.Now;
    }
}

public sealed class AuditLog
{
    public DateTime Time { get; }
    public string Action { get; }

    public AuditLog(string action)
    {
        Time = DateTime.Now;
        Action = action;
    }
}

public sealed class QualityMetric
{
    public string Name{get;}
    public double Score{get;}

    public QualityMetric(string name, double score)
    {
        Name = name;
        Score = score;
    }
}
public class EnterpriseSDLCEngine
{
    private List<Requirement> _requirements;
    private Dictionary<int , Workitem> _workItemRegistry;
    private SortedDictionary<SDLCStage , List<Workitem>> _stageBoard;
    private Queue<Workitem> _executionQueue;
    private Stack<BuildSnapshot> _rollbackStack;
    private HashSet<string> _uniqueTestSuites;
    private LinkedList<AuditLog> _auditLedger;
    private SortedList<double, QualityMetric> _releaseScoreboard;
    private int _requirementCounter;
    private int _workItemCounter;

    public EnterpriseSDLCEngine(){
        _requirements = new List<Requirement>();
        _workItemRegistry = new Dictionary<int, Workitem>();
        _stageBoard = new SortedDictionary<SDLCStage, List<Workitem>>();
        foreach (SDLCStage stage in Enum.GetValues(typeof(SDLCStage)))
            {
                _stageBoard.Add(stage, new List<WorkItem>());
            }
            _executionQueue = new Queue<WorkItem>();
            _rollbackStack = new Stack<BuildSnapshot>();
            _uniqueTestSuites = new HashSet<string>();
            _auditLedger = new LinkedList<AuditLog>();
            _releaseScoreboard = new SortedList<double, QualityMetric>();
            _requirementCounter = 0;
            _workItemCounter = 0;

    }
    public void AddRequirement(string title, RiskLevel risk)
        {
            Requirement req = new Requirement(_requirementCounter, title, risk);
            _requirementCounter++;
            _requirements.Add(req);

            AuditLog log = new AuditLog($"Requirement Added: {title} with Risk {risk}");
            _auditLedger.AddLast(log);
        }

        public WorkItem CreateWorkItem(string name, SDLCStage stage)
        {
            WorkItem newItem = new WorkItem(_workItemCounter, name, stage);
            _workItemCounter++;

            _workItemRegistry.Add(newItem.Id, newItem);
            _stageBoard[stage].Add(newItem);

            AuditLog log = new AuditLog($"WorkItem Created: {name} at Stage {stage}");
            _auditLedger.AddLast(log);

            return newItem;
        }

        public void AddDependency(int workItemId, int dependsOnId)
        {
            if (_workItemRegistry.ContainsKey(workItemId) && _workItemRegistry.ContainsKey(dependsOnId))
            {
                WorkItem item = _workItemRegistry[workItemId];
                item.DependencyIds.Add(dependsOnId);

                AuditLog log = new AuditLog($"Dependency added: Item {workItemId} now depends on {dependsOnId}");
                _auditLedger.AddLast(log);
            }
        }

        public void PlanStage(SDLCStage stage)
        {
            List<WorkItem> itemsInStage = _stageBoard[stage];

            var eligibleItems = itemsInStage.Where(item =>
                item.DependencyIds.All(depId =>
                    _workItemRegistry.ContainsKey(depId) &&
                    (int)_workItemRegistry[depId].Stage > (int)stage
                )
            ).ToList();

            foreach (var item in eligibleItems)
            {
                _executionQueue.Enqueue(item);
            }

            AuditLog log = new AuditLog($"Planned stage: {stage}");
            _auditLedger.AddLast(log);
        }

        public void ExecuteNext()
        {
            if (_executionQueue.Count == 0) return;

            WorkItem item = _executionQueue.Dequeue();
            SDLCStage previousStage = item.Stage;
            item.Stage = (SDLCStage)((int)previousStage + 1);
            _stageBoard[previousStage].Remove(item);
            _stageBoard[item.Stage].Add(item);

            AuditLog log = new AuditLog($"Executed WorkItem {item.Id}: {previousStage} -> {item.Stage}");
            _auditLedger.AddLast(log);
        }

        public void RegisterTestSuite(string suiteId)
        {
            if (_uniqueTestSuites.Add(suiteId))
            {
                AuditLog log = new AuditLog($"Test Suite Registered: {suiteId}");
                _auditLedger.AddLast(log);
            }
        }

        public void DeployRelease(string version)
        {
            BuildSnapshot snapshot = new BuildSnapshot(version);
            _rollbackStack.Push(snapshot);

            AuditLog log = new AuditLog($"Deployment Completed: Version {version}");
            _auditLedger.AddLast(log);
        }

        public void RollbackRelease()
        {
            if (_rollbackStack.Count == 0) return;

            BuildSnapshot snapshot = _rollbackStack.Pop();
            string version = snapshot.Version;

            AuditLog log = new AuditLog($"Rollback executed for version: {version}");
            _auditLedger.AddLast(log);
        }

        public void RecordQualityMetric(string metricName, double score)
        {
            if (!_releaseScoreboard.ContainsKey(score))
            {
                QualityMetric metric = new QualityMetric(metricName, score);
                _releaseScoreboard.Add(score, metric);
            }
        }

        public void PrintAuditLedger()
        {
            foreach (var log in _auditLedger)
            {
                Console.WriteLine($"[{log.Time}] {log.Action}");
            }
        }

        public void PrintReleaseScoreboard()
        {
            for (int i = _releaseScoreboard.Count - 1; i >= 0; i--)
            {
                var metric = _releaseScoreboard.Values[i];
                var scoreKey = _releaseScoreboard.Keys[i];
                
                Console.WriteLine($"{metric.Name}: {scoreKey:F2}");
            }
        }

}
class Program
{
    static void Main()
    {
        
    }
}