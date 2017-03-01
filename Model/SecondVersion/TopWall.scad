//Top_Wall();
module Top_Wall(){
    union(){
        cube(size = [33,33,3], center = true);  
        
        for( i = [0:5]){
            rotate( i*360/5, [0, 0, 1])
           translate( [0,5,7.2] )    
            cube(size = [5,2,17],center=true);
            }   
    }
}