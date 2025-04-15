
namespace SpaceInvaders
{
    public class CollisionResolver
    {
        public CollisionResolver() { }
        public void Resolve(List<Entity> list1, List<Entity> list2)
        {
            // we have to iterate backwards if we modify the lists (in this case remove elements) - and not use foreach
            // Another approach is to collect entities to be removed in a separate list and then remove them after completing the iteration
            for (int i = list1.Count - 1; i >= 0; i--)
            {
                Entity e1 = list1[i];
                for (int j = list2.Count - 1; j >= 0; j--)
                {
                    Entity e2 = list2[j];
                    if (IsColliding(e1.Position.x, e1.Position.y, e1.Width, e1.Height, e2.Position.x, e2.Position.y, e2.Width, e2.Height))
                    {
                        if(e1 is UFO || e2 is UFO)
                        {
                            GlobalData.XP += 10;
                        }
                        list1.RemoveAt(i);
                        list2.RemoveAt(j);
                        break;
                    }
                }
            }
        }

        public void ResolveOutsideBounds(List<Entity> list)
        {
            for(int i = list.Count - 1; i>=0; i--)
            {
                Entity e = list[i];
                
                if (e.Position.y < 0 - e.Height || e.Position.y > GlobalData.ClientHeight || e.Position.x < 0 - e.Width || e.Position.x > GlobalData.ClientWidth)
                {
                    list.RemoveAt(i);
                }

            }
        }
        public bool IsColliding(double x1, double y1, int width1, int height1, double x2, double y2, int width2, int height2)
        {

            return !(x1 + width1 <= x2 ||  // Rectangle 1 is to the left of Rectangle 2
                     x2 + width2 <= x1 ||  // Rectangle 2 is to the left of Rectangle 1
                     y1 + height1 <= y2 || // Rectangle 1 is above Rectangle 2
                     y2 + height2 <= y1);  // Rectangle 2 is above Rectangle 1
            
        }


    }
}
