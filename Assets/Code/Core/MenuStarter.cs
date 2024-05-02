using Core;
using Core.Factory;
using Core.SceneLoader;
using Core.StateMachine;
using Core.StateMachine.StateMachines.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuStarter : MonoBehaviour
{
    private IStateMachine _projectStateMachine;
    private BootstrapperFactory _bootstrapperFactory;
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(
            IStateMachine projectStateMachine,
            BootstrapperFactory bootstrapperFactory,
            ISceneLoader sceneLoader)
    {
        _projectStateMachine = projectStateMachine;
        _bootstrapperFactory = bootstrapperFactory;
        _sceneLoader = sceneLoader;
    }

    private void Awake()
    {
        var projectStarter = FindObjectOfType<Bootstrapper>();

        if (projectStarter != null) return;

        _bootstrapperFactory.CreateBootstrapper();
    }

    private void Start()
    {
        _projectStateMachine.RegisterState<MenuState>(new MenuState(_projectStateMachine, _sceneLoader));
        _projectStateMachine.Enter<MenuState>();
    }
}
