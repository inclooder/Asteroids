using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public class EntityManager
    {
        private int lastEntId = 0;
        private Dictionary<System.Type, Dictionary<int,List<EntityComponent>>> components_store = new Dictionary<System.Type, Dictionary<int,List<EntityComponent>>>();

        public int CreateEntity()
        {
            return lastEntId++;
        }

        public void RemoveEntity(int ent_id)
        {
          
            foreach (System.Type component_type in components_store.Keys)
            {
                components_store[component_type].Remove(ent_id);    
            }
        }

        private Dictionary<int, List<EntityComponent>> GetComponentStore(System.Type store_type)
        {
            if (components_store.ContainsKey(store_type))
            {
                return components_store[store_type];
            }
            else
            {
                Dictionary<int, List<EntityComponent>> component_store = new Dictionary<int, List<EntityComponent>>();
                components_store[store_type] = component_store;
                return component_store;
           
            }
      
        }

        public void AddComponent(int entity_id, EntityComponent component)
        {
            Dictionary<int, List<EntityComponent>> component_store = GetComponentStore(component.GetType());
            if (!component_store.ContainsKey(entity_id))
            {
                List<EntityComponent> components_list = new List<EntityComponent>();
                component_store[entity_id] = components_list;
            }
            component_store[entity_id].Add(component);
        }

        public bool HasComponent<T>(int entity_id)
        {
            Dictionary<int, List<EntityComponent>> component_store = GetComponentStore(typeof(T));
            return component_store.ContainsKey(entity_id);
        }
        public EntityComponent[] GetComponentsOfType(int entity_id, System.Type component_type)
        {
            Dictionary<int, List<EntityComponent>> component_store = GetComponentStore(component_type);
            if (!component_store.ContainsKey(entity_id))
            {
                return new EntityComponent[0];
            }
            else
            {
                return component_store[entity_id].ToArray();
            }
            
        }

        public int[] GetEntitiesWithComponent(System.Type component_type)
        {
            return GetComponentStore(component_type).Keys.ToArray();
        }

    }
}
