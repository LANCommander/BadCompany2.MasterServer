namespace BadCompany2.MasterServer;

public class Packet
{
    private bool _count;
    private bool _first;
    private bool _encoded;
    private bool _delayed;
    private int _delayTime;
    private string _type;
    private uint _type2;
    private int _length;
    private string _varBuffer;
    private ushort _errorCount = 0;

    public Packet(string createType, uint createType2, int createLength, string createData)
    {
        _type = createType;
        _type2 = createType2;
        _length = createLength;
        _varBuffer = createData;
    }

    public Packet(string createType, uint createType2, bool count = true, bool first = true)
    {
        _type = createType;
        _type2 = createType2;
        _count = count;
        _first = first;
        _length = 12;
    }

    public Packet(Packet packet)
    {
        _type = packet._type;
        _type2 = packet._type2;
        _count = packet._count;
        _first = packet._first;
        _encoded = packet._encoded;
        _delayed = packet._delayed;
        _delayTime = packet._delayTime;
        _length = packet._length;
        _varBuffer = packet._varBuffer;
    }

    public string GetVariable(string varName) => _varBuffer; // Placeholder

    public void SetVariable(string varName, string varValue, bool skipCheck = true) { /* Implementation */ }

    public void SetVariable(string varName, int varValue, bool skipCheck = true) { /* Implementation */ }

    public void AddError(string errorCode, string localizedMessage)
    {
        SetVariable("errorContainer.[]", "0");
        SetVariable("errorCode", errorCode);
        SetVariable("localizedMessage", localizedMessage);
    }
    
    public void AddError(string errorCode, string localizedMessage, string fieldName, string fieldError, string errorType)
    {
        _errorCount++;
        
        SetVariable("errorContainer.[]", _errorCount.ToString());
        SetVariable("errorCode", errorCode);
        SetVariable("localizedMessage", localizedMessage);
        SetVariable($"errorContainer.{_errorCount - 1}.fieldName", fieldName);
        SetVariable($"errorContainer.{_errorCount - 1}.fieldError", fieldError);
        SetVariable($"errorContainer.{_errorCount - 1}.value", errorType);
    }

    public void SetType2(uint type2) => _type2 = type2;

    public int GetNumberOfKeys() => 0; // Placeholder

    public override string ToString() => _varBuffer;

    public string GetData() => _varBuffer;

    public string GetType() => _type;

    public uint GetType2() => _type2;

    public int GetLength() => _length;

    public bool IsEncoded => _encoded;

    public void SetEncoded(bool flag) => _encoded = flag;

    public bool IsDelayed => _delayed;

    public int GetDelayTime() => _delayTime;

    public void SetDelayed(bool flag, int seconds) 
    { 
        _delayed = flag; 
        _delayTime = seconds; 
    }

    public bool GetValCount() => _count;

    public bool GetValFirst() => _first;
}
