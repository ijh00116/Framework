using System;
using System.Collections;
using System.Collections.Generic;


public abstract class Architecture<T>:IArchitecture where T : Architecture<T>,new()
{
    private static T mArchitecture;

    private IOCContainer mContainer = new IOCContainer();

    public static IArchitecture Interface
    {
        get
        {
            if(mArchitecture==null)
            {
                MakeSureArchitecture();
            }
            return mArchitecture;
        }
    }

    static void MakeSureArchitecture()
    {
        mArchitecture = new T();
    }

    protected abstract void Init();

    public void RegisterSystem<TSystem>(TSystem system)where TSystem:ISystem
    {
        system.SetArchitecture(this);
        mContainer.Register<TSystem>(system);

        system.Init();
    }

    public void RegisterModel<Tmodel>(Tmodel model)where Tmodel:IModel
    {
        model.SetArchitecture(this);
        mContainer.Register<Tmodel>(model);

        model.Init();
    }
    public TModel GetModel<TModel>() where TModel:class,IModel
    {
        return mContainer.Get<TModel>();
    }
}

public abstract class AbstractSystem : ISystem
{
    private IArchitecture mArchitecture;
    public IArchitecture GetArchitecture()
    {
        return mArchitecture;
    }

    public void SetArchitecture(IArchitecture architecture)
    {
        mArchitecture=architecture;
    }
    void ISystem.Init()
    {
        OnInit();
    }

    protected abstract void OnInit();
}

public abstract class AbstractModel : IModel
{
    private IArchitecture mArchitecture;
    IArchitecture IBelongToArchitecture.GetArchitecture()
    {
        return mArchitecture;
    }

    void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
    {
        mArchitecture = architecture;
    }

    void IModel.Init()
    {
        OnInit();
    }

    protected abstract void OnInit();
}
#region Interface
public interface IArchitecture
{
    T GetModel<T>() where T : class, IModel;
}

public interface IModel : IBelongToArchitecture, ICanSetArchitecture, ICanGetUtility, ICanSendEvent
{
    void Init();
}

public interface ISystem:IBelongToArchitecture, ICanSetArchitecture,ICanGetSystem,ICanGetModel,ICanGetUtility,ICanRegisterEvent,ICanSendEvent
{
    void Init();
}

public interface IBelongToArchitecture
{
    IArchitecture GetArchitecture();
}
public interface ICanSetArchitecture
{
    void SetArchitecture(IArchitecture architecture);
}
public interface ICanGetSystem : IBelongToArchitecture
{
  
}
public interface ICanGetModel : IBelongToArchitecture
{
}
public interface ICanGetUtility : IBelongToArchitecture
{
}
public interface ICanRegisterEvent : IBelongToArchitecture
{
}
public interface ICanSendEvent : IBelongToArchitecture
{
}
public interface IUnRegister
{
    void UnRegister();
}
#endregion

#region IOC
public class IOCContainer
{
    private Dictionary<Type, object> mInstances = new Dictionary<Type, object>();

    public void Register<T>(T instance)
    {
        var key = typeof(T);

        if (mInstances.ContainsKey(key))
        {
            mInstances[key] = instance;
        }
        else
        {
            mInstances.Add(key, instance);
        }
    }

    public T Get<T>() where T : class
    {
        var key = typeof(T);

        if (mInstances.TryGetValue(key, out var retInstance))
        {
            return retInstance as T;
        }

        return null;
    }
}
#endregion
#region Extension

public static class CanGetModelExtension
{
    public static T GetModel<T>(this ICanGetModel self)where T:class, IModel
    {
        return self.GetArchitecture().GetModel<T>();
    }
}
#endregion

#region ETC
public interface IBindableProperty<T> : IReadonlyBindableProperty<T>
{
    new T Value { get; set; }
    void SetValueWithoutEvent(T newValue);
}
public interface IReadonlyBindableProperty<T>
{
    T Value { get; }

    void RegisterWithInitValue(Action<T> action);
    void UnRegister(Action<T> onValueChanged);
    void Register(Action<T> onValueChanged);
}
public class BindableProperty<T>:IBindableProperty<T>
{
    protected T mValue;

    private Action<T> mOnValueChanged = (v) => { };

    public BindableProperty(T defaultValue=default)
    {
        mValue = defaultValue;
    }

    public T Value
    {
        get => GetValue();
        set
        {
            if (value == null && mValue == null) return;
            if (value != null && value.Equals(mValue)) return;

            SetValue(value);
            mOnValueChanged?.Invoke(value);
        }
    }

    protected virtual void SetValue(T newValue)
    {
        mValue = newValue;
    }

    protected virtual T GetValue()
    {
        return mValue;
    }

    public void SetValueWithoutEvent(T newValue)
    {
        mValue = newValue;
    }

    public void RegisterWithInitValue(Action<T> onValueChanged)
    {
        onValueChanged(mValue);
       Register(onValueChanged);
    }

    public void Register(Action<T> onValueChanged)
    {
        mOnValueChanged += onValueChanged;
    }

    public static implicit operator T(BindableProperty<T> property)
    {
        return property.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public void UnRegister(Action<T> onValueChanged)
    {
        mOnValueChanged -= onValueChanged;
    }
}
#endregion