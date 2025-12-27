using Java.Io;
using Java.Lang.Reflect;
using Java.Util;
using Java.Util.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.ElementType;

namespace Gs_Core.Graphstream.Util;

public class Environment : Cloneable
{
    private static readonly Logger logger = Logger.GetLogger(typeof(Environment).GetSimpleName());
    protected string configFileName = "config";
    protected bool configFileRead = false;
    protected Hashtable<string, string> parameters = new Hashtable<string, string>();
    protected bool locked;
    public static Environment GLOBAL_ENV;
    public static Environment GetGlobalEnvironment()
    {
        if (GLOBAL_ENV == null)
            GLOBAL_ENV = new Environment();
        return GLOBAL_ENV;
    }

    public virtual bool IsLocked()
    {
        return locked;
    }

    public virtual string GetParameter(string parameter)
    {
        string p = parameters[parameter];
        return (p == null) ? "" : p;
    }

    public virtual bool HasParameter(string parameter)
    {
        return (parameters[parameter] != null);
    }

    public virtual bool GetBooleanParameter(string parameter)
    {
        int val = GetBooleanParameteri(parameter);
        return (val == 1);
    }

    public virtual int GetBooleanParameteri(string parameter)
    {
        string p = parameters[parameter];
        if (p != null)
        {
            p = p.ToLowerCase();
            if (p.Equals("1"))
                return 1;
            if (p.Equals("true"))
                return 1;
            if (p.Equals("on"))
                return 1;
            if (p.Equals("yes"))
                return 1;
            return 0;
        }

        return -1;
    }

    public virtual double GetNumberParameter(string parameter)
    {
        string p = parameters[parameter];
        if (p != null)
        {
            try
            {
                return Double.ParseDouble(p);
            }
            catch (NumberFormatException e)
            {
                return 0;
            }
        }

        return 0;
    }

    public virtual int GetParameterCount()
    {
        return parameters.Count;
    }

    public virtual HashSet<string> GetParametersKeySet()
    {
        return parameters.KeySet();
    }

    public virtual Environment Clone()
    {
        Environment e = new Environment();
        e.configFileName = configFileName;
        e.configFileRead = configFileRead;
        e.locked = locked;
        foreach (string key in parameters.KeySet())
        {
            e.parameters.Put(key, parameters[key]);
        }

        return e;
    }

    public virtual void SetParameter(string parameter, string value)
    {
        if (!locked)
        {
            parameters.Put(parameter, value);
        }
        else
        {
            if (parameters[parameter] != null)
                parameters.Put(parameter, value);
        }
    }

    public virtual void LockEnvironment(bool on)
    {
        locked = on;
    }

    public virtual void InitializeFieldsOf(object @object)
    {
        Method[] methods = @object.GetType().GetMethods();
        foreach (Method method in methods)
        {
            if (method.GetName().StartsWith("set"))
            {
                Class<TWildcardTodo>[] types = method.GetParameterTypes();
                if (types.Length == 1)
                {
                    string name = method.GetName().Substring(3, 4).ToLowerCase() + method.GetName().Substring(4);
                    string value = parameters[name];
                    if (value != null)
                    {
                        InvokeSetMethod(@object, method, types, name, value);
                    }
                }
            }
        }
    }

    public virtual void InitializeFieldsOf(object @object, params string[] fieldList)
    {
        Method[] methods = @object.GetType().GetMethods();
        HashSet<string> names = new HashSet<string>();
        foreach (string s in fieldList)
            names.Add(s);
        foreach (Method method in methods)
        {
            if (method.GetName().StartsWith("set"))
            {
                Class<TWildcardTodo>[] types = method.GetParameterTypes();
                if (types.Length == 1)
                {
                    string name = method.GetName().Substring(3, 4).ToLowerCase() + method.GetName().Substring(4);
                    if (names.Contains(name))
                    {
                        string value = parameters[name];
                        if (value != null)
                        {
                            InvokeSetMethod(@object, method, types, name, value);
                        }
                    }
                }
            }
        }
    }

    protected virtual void InitializeFieldsOf(object @object, Collection<string> fieldList)
    {
        Method[] methods = @object.GetType().GetMethods();
        foreach (Method method in methods)
        {
            if (method.GetName().StartsWith("set"))
            {
                Class<TWildcardTodo>[] types = method.GetParameterTypes();
                if (types.Length == 1)
                {
                    string name = method.GetName().Substring(3).ToLowerCase();
                    if (fieldList.Contains(name))
                    {
                        string value = parameters[name];
                        if (value != null)
                        {
                            InvokeSetMethod(@object, method, types, name, value);
                        }
                    }
                }
            }
        }
    }

    protected virtual void InvokeSetMethod(object @object, Method method, Class<TWildcardTodo>[] types, string name, string value)
    {
        try
        {
            if (types[0] == Long.TYPE)
            {
                try
                {
                    long val = Long.ParseLong(value);
                    method.Invoke(@object, new long (val));
                }
                catch (NumberFormatException e)
                {
                    logger.Warning(String.Format("cannot set '%s' to the value '%s', values is not a long%n", method.ToString(), value));
                }
            }
            else if (types[0] == Integer.TYPE)
            {
                try
                {
                    int val = (int)Double.ParseDouble(value);
                    method.Invoke(@object, new int (val));
                }
                catch (NumberFormatException e)
                {
                    logger.Warning(String.Format("cannot set '%s' to the value '%s', values is not a int%n", method.ToString(), value));
                }
            }
            else if (types[0] == Double.TYPE)
            {
                try
                {
                    double val = Double.ParseDouble(value);
                    method.Invoke(@object, new Double(val));
                }
                catch (NumberFormatException e)
                {
                    logger.Warning(String.Format("cannot set '%s' to the value '%s', values is not a double%n", method.ToString(), value));
                }
            }
            else if (types[0] == Float.TYPE)
            {
                try
                {
                    float val = Float.ParseFloat(value);
                    method.Invoke(@object, new float (val));
                }
                catch (NumberFormatException e)
                {
                    logger.Warning(String.Format("cannot set '%s' to the value '%s', values is not a float%n", method.ToString(), value));
                }
            }
            else if (types[0] == Boolean.TYPE)
            {
                try
                {
                    bool val = false;
                    value = value.ToLowerCase();
                    if (value.Equals("1") || value.Equals("true") || value.Equals("yes") || value.Equals("on"))
                        val = true;
                    method.Invoke(@object, new bool (val));
                }
                catch (NumberFormatException e)
                {
                    logger.Warning(String.Format("cannot set '%s' to the value '%s', values is not a boolean%n", method.ToString(), value));
                }
            }
            else if (types[0] == typeof(string))
            {
                method.Invoke(@object, value);
            }
            else
            {
                logger.Warning(String.Format("cannot match parameter '%s' and the method '%s'%n", value, method.ToString()));
            }
        }
        catch (InvocationTargetException ite)
        {
            logger.Warning(String.Format("cannot invoke method '%s' : invocation targer error : %s%n", method.ToString(), ite.GetMessage()));
        }
        catch (IllegalAccessException iae)
        {
            logger.Warning(String.Format("cannot invoke method '%s' : illegal access error : %s%n", method.ToString(), iae.GetMessage()));
        }
    }

    public virtual void PrintParameters(PrintStream @out)
    {
        @out.Println(ToString());
    }

    public virtual void PrintParameters()
    {
        PrintParameters(System.@out);
    }

    public virtual string ToString()
    {
        return parameters.ToString();
    }

    public virtual void ReadCommandLine(string[] args)
    {
        ReadCommandLine(args, null);
    }

    public virtual void ReadCommandLine(string[] args, Collection<string> trashcan)
    {
        foreach (string arg in args)
        {
            bool startsWithMinus = arg.StartsWith("-");
            int equalPos = arg.IndexOf('=');
            string value = "true";
            if (equalPos >= 0)
            {
                value = arg.Substring(equalPos + 1);
                if (startsWithMinus)
                {
                    arg = arg.Substring(1, equalPos);
                }
                else
                {
                    arg = arg.Substring(0, equalPos);
                }

                parameters.Put(arg, value);
            }
            else
            {
                if (startsWithMinus)
                {
                    arg = arg.Substring(1);
                    parameters.Put(arg, value);
                }
                else
                {
                    ReadConfigFile(arg, trashcan);
                }
            }
        }
    }

    protected virtual void ReadConfigFile(string filename, Collection<string> trashcan)
    {
        BufferedReader br;
        int count = 0;
        try
        {
            br = new BufferedReader(new FileReader(filename));
            string str;
            while ((str = br.ReadLine()) != null)
            {
                count++;
                if (str.Length > 0 && !str.Substring(0, 1).Equals("#"))
                {
                    string[] val = str.Split("=");
                    if (val.Length != 2)
                    {
                        if (val.Length == 1)
                        {
                            parameters.Put(val[0].Trim(), "true");
                        }
                        else
                        {
                            logger.Warning(String.Format("Something is wrong with the configuration file \"%s\"near line %d :\n %s", filename, count, str));
                            if (trashcan != null)
                            {
                                trashcan.Add(str);
                            }
                        }
                    }
                    else
                    {
                        string s0 = val[0].Trim();
                        string s1 = val[1].Trim();
                        parameters.Put(s0, s1);
                    }
                }
            }
        }
        catch (FileNotFoundException fnfe)
        {
            System.err.Printf("Tried to open \"%s\" as a config file: file not found.%n", filename);
            if (trashcan != null)
            {
                trashcan.Add(filename);
            }
        }
        catch (IOException ioe)
        {
            ioe.PrintStackTrace();
            System.Exit(0);
        }
    }

    public virtual void WriteParameterFile(string fileName)
    {
        BufferedWriter bw = new BufferedWriter(new FileWriter(fileName));
        HashSet<string> ks = parameters.KeySet();
        foreach (string key in ks)
        {
            bw.Write(key + " = " + parameters[key]);
            bw.NewLine();
        }

        bw.Dispose();
    }

    protected virtual void ReadConfigurationFile()
    {
        try
        {
            ReadParameterFile(configFileName);
            configFileRead = true;
        }
        catch (IOException ioe)
        {
            logger.Log(Level.WARNING, String.Format("%-5s : %s : %s\n", "Warning", "Environment", "Something wrong while reading the configuration file."), ioe);
        }
    }

    public virtual void ReadParameterFile(string fileName)
    {
        BufferedReader br;
        int count = 0;
        br = new BufferedReader(new FileReader(fileName));
        string str;
        while ((str = br.ReadLine()) != null)
        {
            count++;
            if (str.Length > 0 && !str.StartsWith("#"))
            {
                string[] val = str.Split("=");
                if (val.Length != 2)
                {
                    logger.Warning(String.Format("%-5s : %s : %s\n", "Warn", "Environment", "Something is wrong in your configuration file near line " + count + " : \n" + Arrays.ToString(val)));
                }
                else
                {
                    string s0 = val[0].Trim();
                    string s1 = val[1].Trim();
                    SetParameter(s0, s1);
                }
            }
        }

        br.Dispose();
    }
}
