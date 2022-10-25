(function($){
    var turn = 1,
    colParent, countPlayed, numberCol;

    $('#turn-player').html('Tour du joueur : '+turn);

    $('label').click(function(){

        $(this).prop("checked", false);

        labelID = $(this).attr('for');
        numberCol = labelID.slice(0,-1);

        colParent = $('#'+labelID).parent().attr('id');
        countPlayed = $('#'+colParent+' .played').length;

        positionPlayed = 6 - countPlayed;


        if(positionPlayed != 0){
            $('#'+numberCol+positionPlayed).addClass('player-'+turn).addClass('played');
            $('#'+numberCol+positionPlayed).prop("checked", true);
        }else{
            alert('Colonne pleine !');
        }


        $('#turn-player').html();

        if(turn == 1){turn = 2; $('#turn-player').html('Tour du joueur : '+turn); return true;}
        if(turn == 2){turn = 1; $('#turn-player').html('Tour du joueur : '+turn); return true;}


    });

var hauteur = 8, largeur = 8;
var player = 1;

//initialisation de la grille
var grid = [];

//compte le nombre de pions dans une colonne
function columnNumber(col){
    var count = 0;
    for(var i=0; i<hauteur; i++)
        if(grid[col][i]!==0)
            count++;
    return count;
}

function putPiece(col, player){
    var i;
    console.log(columnNumber(col));
    if(columnNumber(col)===hauteur)
        return -1;

    for(i=0; i<hauteur; i++){
        if(grid[col][i]!==0){
            grid[col][i-1]=player;
            return i-1;
        }
    }
    grid[col][i-1]=player;
    return i-1;
}

function nextTurn(){
    player = player===1?2:1;
}

function onMouseDown(event) {
    var col = event.point.x/((width+offset)*largeur)*largeur;
    col = Math.floor(col);
    col = col>=largeur?largeur-1:col;

    var y = putPiece(col,player);
    if(y!=-1){
        console.log(isWinning(col,y));
        nextTurn();
    }
}

function isWinning(x,y){
    /*//horizontal
    var count = 1;
    if(x-1>=0)
        count+=checkDirection(player,x-1,y,-1,0);
    if(x+1<largeur-1)
        count+=checkDirection(player,x+1,y,1,0);

    if(count>=4)return true;


    //diag up-left/down-right
    count = 1;
    if(x-1>=0 && y-1>=0)
        count+=checkDirection(player,x-1,y-1,-1,-1);
    if(x+1<largeur-1 && y+1<hauteur-1)
        count+=checkDirection(player,x+1,y+1,1,1);
    if(count>=4)return true;

    //vertical
    count = 1;
    if(y-1>=0)
        count+=checkDirection(player,x,y-1,0,-1);
    if(y+1<hauteur-1)
        count+=checkDirection(player,x,y+1,0,1);
    if(count>=4)return true;
*/
    //diag down-left/up-right
    count = 1;
    if(x-1>=0 && y+1<largeur-1)
        count+=checkDirection(player,x-1,y+1,-1,1);
    if(x+1<largeur-1 && y-1<=0)
        count+=checkDirection(player,x+1,y-1,1,-1);
    if(count>=4)return true;
    return false;
}

//fait une verification récursive dans une direction
function checkDirection(player,x,y,dx,dy){
    return grid[x][y]===player?
        (((x+dx>=0 && x+dx<largeur)&&(y+dy>=0 && y+dy<largeur)))?
        1+checkDirection(player,x+dx,y+dy,dx,dy):1:0;
}

})(jQuery);

