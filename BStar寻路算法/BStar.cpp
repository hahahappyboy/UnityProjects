#include<bits/stdc++.h>
using namespace std;

const bool showmap=1;//改为 1 可以逐步查看路径

const int SIZE=500;

const short direx[]={1,0,-1,0};
const short direy[]={0,1,0,-1};

struct Point
{
    int x,y,step;//x，y是坐标，step是步数
    short WD,D;//WD是墙的方法4表示没墙，D是当前点走的方向
    const bool operator == (Point ob)
    {return x==ob.x && y==ob.y;}
    void Scan () {scanf ("%d %d",&x,&y);}
    void Print() {printf("%d %d\n",x,y);}
    void Walk(short dire)
    {x+=direx[dire],y+=direy[dire];}
    Point Next(short dire)
    {return Point{x+direx[dire],y+direy[dire],step,WD};}
}startPoint,endPoint;

int n,m;
bool mapn[SIZE+5][SIZE+5],visit[SIZE+5][SIZE+5];
queue<Point> B_star;

void maprint(Point ob)//展示路径
{
    for(int i=1;i<=n;i++)
    {
        for(int j=1;j<=m;j++)
            if(mapn[i][j]) cout<<setw(3)<<"#";//障碍物用#打印
            else if(Point{j,i}==ob) cout<<setw(3)<<ob.step;//当前点走的步数
            else if(Point{j,i} == endPoint) cout << setw(3) << "E";//开始点
            else if(Point{j,i} == startPoint) cout << setw(3) << "S";//结束点
            else if(visit[i][j]) cout<<setw(3)<<"1";//已经访问过的点
            else cout<<setw(3)<<".";//还没走的路用点
        printf("\n");
    }
}

bool NW(Point ob)//near the wall
{
    for(short i=0;i<4;i++)
    {
        Point rear=ob;
        rear.Walk(i);
        if(mapn[rear.y][rear.x]) return 1;
    }
    return 0;
}

bool Allowed(Point ob)
{return !mapn[ob.y][ob.x] && !visit[ob.y][ob.x];}

bool AtWall(Point ob)
{return mapn[ob.y][ob.x];}

short SD(Point ob)//确定当前点本应该朝着哪个方向走
{
    if(abs(ob.x - endPoint.x) >= abs(ob.y - endPoint.y))
    {
        if(ob.x < endPoint.x) return 0;//右边
        if(ob.x > endPoint.x) return 2;//左边
    }
    if(ob.y < endPoint.y) return 1;//下
    return 3;//上
}

int main()
{
    memset(mapn,1,sizeof mapn);
    scanf("%d %d",&n,&m);//地图大小
    for(int i=1;i<=n;i++)
        for(int j=1;j<=m;j++)
            cin>>mapn[i][j];
    startPoint.Scan(), startPoint.step=0, startPoint.WD= startPoint.D=4;//起始点
    endPoint.Scan();//终点
    B_star.push(startPoint);//其实点如队列
    if(showmap) system("cls");
    while(!B_star.empty())//队列不为空
    {
        Point now=B_star.front();B_star.pop();//出队
        //如果是终点就找到了
        if(now == endPoint) {printf("B-star ans: %d\n", now.step);return 0;}
        //如果是障碍物或则走过了就不管这个点
        if(!Allowed(now)) continue;
        //访问这个点
        visit[now.y][now.x]=1;
        if(showmap)
        {
            maprint(now);
            system("pause");
            system("cls");
        }
        /*
         0 右
         1 下
         2 左
         3 上
        */
        if(abs(now.x - endPoint.x) >= abs(now.y - endPoint.y)) //当前点离终点x方向比y方向远的话，就看走左边还是右边
        {   //now.x < endPoint.x终点在当前点的右边，并且now.Next(0)右边没有障碍也没走过，就朝右走
            if(now.x < endPoint.x && Allowed(now.Next(0)))
            {
                Point rear=now.Next(0);
                rear.step++,rear.WD=rear.D=4;
                B_star.push(rear);//入队列
                continue;
            }
            if(now.x > endPoint.x && Allowed(now.Next(2)))//朝左走
            {
                Point rear=now.Next(2);
                rear.step++,rear.WD=rear.D=4;
                B_star.push(rear);
                continue;
            }
        }
        else//当前点离终点y方向比x方向远的话，就看走上边还是下边
        {
            if(now.y < endPoint.y && Allowed(now.Next(1)))//朝下走
            {
                Point rear=now.Next(1);
                rear.step++,rear.WD=rear.D=4;
                B_star.push(rear);
                continue;
            }
            if(now.y > endPoint.y && Allowed(now.Next(3)))//朝上走
            {
                Point rear=now.Next(3);
                rear.step++,rear.WD=rear.D=4;
                B_star.push(rear);
                continue;
            }
        }
        /*
            0 右
            1 下
            2 左
            3 上
        */
        //不能径直走 ,SD(now)找到墙在now的哪个方向
        if(now.WD==4 && AtWall(now.Next(SD(now))))//第一次撞到墙
        {
            if(SD(now)==0) //墙在右边
            {   //分为上下路，入队
                Point rear;
                rear=now.Next(1),rear.D=1,rear.step++,rear.WD=0,B_star.push(rear);
                rear=now.Next(3),rear.D=3,rear.step++,rear.WD=0,B_star.push(rear);
                continue;
            }
            if(SD(now)==1) //墙在下边
            {
                Point rear;
                rear=now.Next(0),rear.D=0,rear.step++,rear.WD=1,B_star.push(rear);
                rear=now.Next(2),rear.D=2,rear.step++,rear.WD=1,B_star.push(rear);
                continue;
            }
            if(SD(now)==2) //墙在左边
            {
                Point rear;
                rear=now.Next(1),rear.D=1,rear.step++,rear.WD=2,B_star.push(rear);
                rear=now.Next(3),rear.D=3,rear.step++,rear.WD=2,B_star.push(rear);
                continue;
            }
            if(SD(now)==3) //墙在上边
            {
                Point rear;
                rear=now.Next(0),rear.D=0,rear.step++,rear.WD=3,B_star.push(rear);
                rear=now.Next(2),rear.D=2,rear.step++,rear.WD=3,B_star.push(rear);
                continue;
            }
        }
            /*
                0 右
                1 下
                2 左
                3 上
               */
        else//早就已经撞到墙了
        {
            if(now.WD==0)//墙在右边
            {
                if(!AtWall(now.Next(0)))//右边根本没墙
                {
                    if(now.D==1)//本来向下走，因为右边没有墙了，就继续往右走，并且往右走后墙就在它上面了
                    {
                        Point rear;
                        rear=now.Next(0),rear.D=0,rear.step++,rear.WD=3,B_star.push(rear);
                        continue;
                    }
                    if(now.D==3)
                    {
                        Point rear;
                        rear=now.Next(0),rear.D=0,rear.step++,rear.WD=1,B_star.push(rear);
                        continue;
                    }
                }
                //右边有墙，沿着 now.D 原本方向继续走
                if(!AtWall(now.Next(now.D)))//能继续走
                {
                    Point rear;
                    rear=now.Next(now.D),rear.D=now.D,rear.step++,rear.WD=0,B_star.push(rear);
                    continue;
                }
                //沿着这个方向上、下、右都不能再走了，就往左走
                Point rear;
                rear=now.Next(2),rear.D=2,rear.step++,rear.WD=now.D,B_star.push(rear);
                continue;
            }
            if(now.WD==1)//墙在下边
            {
                if(!AtWall(now.Next(1)))//下边根本没墙
                {
                    if(now.D==0)//向右走
                    {
                        Point rear;
                        rear=now.Next(1),rear.D=1,rear.step++,rear.WD=2,B_star.push(rear);
                        continue;
                    }
                    if(now.D==2)//向左走
                    {
                        Point rear;
                        rear=now.Next(1),rear.D=1,rear.step++,rear.WD=0,B_star.push(rear);
                        continue;
                    }
                }
                //下边有墙，沿着 now.D 继续走
                if(!AtWall(now.Next(now.D)))//能继续走
                {
                    Point rear;
                    rear=now.Next(now.D),rear.D=now.D,rear.step++,rear.WD=1,B_star.push(rear);
                    continue;
                }
                //沿着这个方向不能再走了
                Point rear;
                rear=now.Next(3),rear.D=3,rear.step++,rear.WD=now.D,B_star.push(rear);
                continue;
            }
            /*
                0 右
                1 下
                2 左
                3 上
               */
            if(now.WD==2)//墙在左边
            {
                if(!AtWall(now.Next(2)))//左边根本没墙
                {
                    if(now.D==1)//本来向下走
                    {
                        Point rear;
                        rear=now.Next(2),rear.D=2,rear.step++,rear.WD=3,B_star.push(rear);
                        continue;
                    }
                    if(now.D==3)
                    {
                        Point rear;
                        rear=now.Next(2),rear.D=2,rear.step++,rear.WD=1,B_star.push(rear);
                        continue;
                    }
                }
                //右边有墙，沿着 now.D 继续走
                if(!AtWall(now.Next(now.D)))//能继续走
                {
                    Point rear;
                    rear=now.Next(now.D),rear.D=now.D,rear.step++,rear.WD=2,B_star.push(rear);
                    continue;
                }
                //沿着这个方向不能再走了
                Point rear;
                rear=now.Next(0),rear.D=0,rear.step++,rear.WD=now.D,B_star.push(rear);
                continue;
            }
            if(now.WD==3)//墙在上边
            {
                if(!AtWall(now.Next(3)))//上边根本没墙
                {
                    if(now.D==0)//向右走
                    {
                        Point rear;
                        rear=now.Next(3),rear.D=3,rear.step++,rear.WD=2,B_star.push(rear);
                        continue;
                    }
                    if(now.D==2)//向左走
                    {
                        Point rear;
                        rear=now.Next(3),rear.D=3,rear.step++,rear.WD=0,B_star.push(rear);
                        continue;
                    }
                }
                //下边有墙，沿着 now.D 继续走
                if(!AtWall(now.Next(now.D)))//能继续走
                {
                    Point rear;
                    rear=now.Next(now.D),rear.D=now.D,rear.step++,rear.WD=3,B_star.push(rear);
                    continue;
                }
                //沿着这个方向不能再走了
                Point rear;
                rear=now.Next(1),rear.D=1,rear.step++,rear.WD=now.D,B_star.push(rear);
                continue;
            }
            /*
                0 右
                1 下
                2 左
                3 上
               */
        }
    }
    printf("No way!\n");
    return 0;
}