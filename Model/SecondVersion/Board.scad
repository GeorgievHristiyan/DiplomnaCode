//Board();
module Board(){
    union(){
        board();
        funnel();
    }
    module board(){
       union(){
           difference(){ 
               translate([0,0,0])
               cube(size = [30,30,1], center = true);  
               
               translate([8,0,0])
               cylinder(h=4, r=2, center=true, $fn=1000);
           }
           difference(){
                translate([0,0,1.3])
                cylinder(h=2, r=4.5,center=true, $fn=1000);
                
                 translate([0,0,1.6])
                cylinder(h=2, r=3.5,center=true, $fn=1000);
            }
        }
    }

    module funnel(){
        translate([8,0,-2.2])
        difference(){
            cylinder(h=5, r1=1, r2=3, center=true, $fn=1000);
             
            cylinder(h=6, r1=0.3, r2=2.2, center=true, $fn=1000);
     }
    }
}