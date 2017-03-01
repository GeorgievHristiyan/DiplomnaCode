module LeftAxis(){
    union(){
        rotate([0,90,0])
        cylinder(h=27,r=2,center=true, $fn=100);
        
        rotate([0,90,0])
        translate([0,0,4.5])
        cylinder (h = 22, center=true,r=0.599, $fn=10);
    }
}