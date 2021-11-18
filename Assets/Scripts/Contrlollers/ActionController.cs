
using UnityEngine;
using UnityEngine.AI;

public  class ActionController
{
     private Stats _stats;
     private NavMeshAgent _agent;
     private StateControllers _stateControllers;
     private PlayerView _playerView;
     private AnimationController _animationController;
     private Transform[] _pointPatrol;
     private Transform _player;
     private Transform _targetObj;
     private float _cooldown;
     private float _delay;
     
     public ActionController(StateControllers stateControllers)
     {
          _stateControllers = stateControllers;
          _agent = _stateControllers._agent;
          _animationController = _stateControllers._animationController;
          _player = _stateControllers.transform;
          _stats = _stateControllers._view._stats;
          _cooldown = _stats._coolDown;
          _delay = _stats._delayPatrol;
          _pointPatrol = _stateControllers.pointPatrol;
     }
     public void GetPosition(Vector3 endPos, Transform targetObj)
     {
          _targetObj = targetObj;
          switch (_stateControllers._state)
          {
               case State.Move: _agent.stoppingDistance = 1;  _agent.SetDestination(endPos); break;
               case State.Patrol: _agent.SetDestination(endPos); break;
               case State.Attack: if (Vector3.Distance(_player.position, endPos) > _stats._distanceSkill) 
               {
                    _agent.stoppingDistance = _stats._distanceSkill;
                    _agent.SetDestination(endPos);
               }break;
               case State.Take: _agent.stoppingDistance = 1;  _agent.SetDestination(endPos); break;
          }
     }
     public void ActionState()
     {
          _animationController.MoveAnimation(_agent.velocity.magnitude);
          _cooldown -= Time.deltaTime;
          ExecuteAction();
          
     }
     private void Rotation(Transform targetObj)
     {
          if (targetObj == null) return;
          var relativePos = targetObj.position - _player.position;
          _player.rotation = Quaternion.LookRotation(relativePos);

     }
     private void ExecuteAction()
     {
          if (_agent.remainingDistance <= _agent.stoppingDistance && _agent.velocity.magnitude < 0.1)
          {
               _delay -= Time.deltaTime;
               if (_stateControllers._state == State.Attack && _cooldown <= 0)
               {
                    Rotation(_targetObj);
                    _animationController.AttackVariant(_stats._TypeAttack);
                    _stateControllers._state = State.Stay;
                    _cooldown = _stats._coolDown;
               }
               if(_stateControllers._state == State.Take)
               {
                    Rotation(_targetObj);
                    _animationController.TakeItem();
                    _stateControllers.transform.GetComponent<PlayerView>().SetDataCell(_targetObj.GetComponent<DataObj>()._Data, _targetObj.gameObject);
                    _stateControllers._state = State.Stay;
               }
               if(_stateControllers._state == State.Patrol && _delay <= 0 && _pointPatrol.Length>0)
               {
                    GetPosition(_pointPatrol[Random.Range(0, _pointPatrol.Length)].position, null);
                    _delay = _stats._delayPatrol;
               }
          }
          else if (_agent.remainingDistance >= 20)
          {
               _stateControllers._state = State.Patrol;
          }
     }
}