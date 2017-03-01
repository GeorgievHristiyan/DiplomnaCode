module Barrel(){
    union(){
        translate([0,0,11.5])
        scale([0.7,0.7,2])
        gear();
        translate([0,0,10])
        #cylinder(h=5, r=2, center=true, $fn=100);
        
        barrel();
        
        stands(0,7,-1.5);
       
        translate([0,0,-1])
        cylinder(h=18,r=3.4,center=true,$fn=1000);
       
    }

    module gear(){
      difference(){
        cylinder(d=5,h=0.8,$fn=100);
        translate([0,0,-1]) cylinder(d=0,h=2.8,$fn=100);
      }
        
      for(i=[0:50]){
        rotate([0,0,i*(360/50)]) translate([5/2,0,0]) 
        hull(){ 
              translate([4,0,0])      cylinder(d=0.5,h=0.8,$fn=100);
              translate([-0.1,-1/2,0]) cube([0.1,1,0.8]);
        }
      }   
    }

    module stands(x,y,z){
         for( i = [0:5]){
            rotate( i*360/6, [0, 0, 1])
           translate( [x, y, z] )    
            cylinder(h=5, r=0.6,center=true, $fn=1000);
            }   
    }

    module barrel(){
        difference(){
            translate([0,0,-1])
            cylinder(h=8, r=11, center=true, $fn=100);
            
            translate([0,0,1])
            cylinder(h=6, r=10.5, center=true, $fn=100);
            
            stands(4.3,7,-4);
                  
        }
    }
}