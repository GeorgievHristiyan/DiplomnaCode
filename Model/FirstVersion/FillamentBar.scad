module FillamentBar(){
    union(){
        rotate([0,90,0])
        cylinder(h=93.5, r=10,center=true,$fn=100);

        rotate([0,90,0])
        cylinder(h=100, r=10,center=true,$fn=5);
    }
}