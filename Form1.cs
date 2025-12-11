using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_main_project
{
    public partial class win : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        public win()
        {
            InitializeComponent();
        }
        public Tuple<bool, Button ,int[]> check_en_passant = new Tuple<bool, Button, int[]>(false, default, new int[] { -1, -1 });
        public Button[][] all_squares  = new Button[8][];
        public Button[] white_pieces = new Button[16];
        public Button[] black_pieces = new Button[16];
        public Button[] all_pieces = new Button[32];
        public Button[][][] places_pieces = new Button[8][][];
        public List<Button> threatened_pieces = new List<Button>();
        public void AllSquares()
        {
            this.all_squares = new Button[8][]
            {
                new Button[] { b0, b1, b2, b3, b4, b5, b6, b7 },
                new Button[] { b8, b9, b10, b11, b12, b13, b14, b15 },
                new Button[] { b16, b17, b18, b19, b20, b21, b22, b23 },
                new Button[] { b24, b25, b26, b27, b28, b29, b30, b31 },
                new Button[] { b32, b33, b34, b35, b36, b37, b38, b39 },
                new Button[] { b40, b41, b42, b43, b44, b45, b46, b47 },
                new Button[] { b48, b49, b50, b51, b52, b53, b54, b55 },
                new Button[] { b56, b57, b58, b59, b60, b61, b60, b63 }
            };
        }
        public void WhitePieces()
        {
            this.white_pieces = new Button[]{ w_pawn_a, w_pawn_b, w_pawn_c, w_pawn_d, w_pawn_e, w_pawn_f, w_pawn_g, w_pawn_h, w_queen, w_king, w_knight_1, w_knight_2, w_bishop_1, w_bishop_2, w_rook_1, w_rook_2 };
        }
        public void BlackPieces()
        {
            this.black_pieces = new Button[]{ b_pawn_a, b_pawn_b, b_pawn_c, b_pawn_d, b_pawn_e, b_pawn_f, b_pawn_g, b_pawn_h, b_queen, b_king, b_knight_1, b_knight_2, b_bishop_1, b_bishop_2, b_rook_1, b_rook_2 };
        }
        public void AllPieces() 
        {
            for (int i = 0; i < 16; i++)
            {
                this.all_pieces[i] = this.white_pieces[i];
            }
            for (int j = 16; j < 32; j++)
            {
                this.all_pieces[j] = this.black_pieces[j];
            }
            all_pieces[33] = default;
        }
        public void PlacesPieces()
        {
            // squares will contain all the squares with their index
            Button[][] squares = this.all_squares;
            // pieces will contain all the pieces
            Button[] pieces = this.all_pieces;
            // array will be used to know where are the pieces on the board(in which square)
            Button[][][] places_all_pieces = new Button[8][][];
            // this algorithm will return a 3D array which is places_all_pieces with the pieces
            // and their squares and 2D array indexs_available_squares with 0(available square) and 1 (unavailable square)
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    for(int k = 0; k < 32; k++)
                    {
                        if (squares[i][j].Location == get_piece_square_location(pieces[k]))
                        {
                            this.places_pieces[i][j] = new Button[2] { pieces[k], squares[i][j] };
                            k = 32;
                        }
                    }
                }
            }
        }
        public Boolean is_it_able(Button square, string color_of_piece)
        {
            // x_square is the row of the square (Tag)
            int x_square = Convert.ToInt32(square.Tag);
            // y_square is the column of the square (TabIndex)
            int y_square = square.TabIndex;
            // check_number will be 0 if the square is empty and 1 if the square is full
            Button check_number = this.places_pieces[y_square][x_square][0];
            bool check = default;
            // checking if the square is empty or full
            if (check_number == default)
            {
                check = true;
            }
            else
            {
                string check_color = this.places_pieces[y_square][x_square][0].Tag.ToString();
                if (check_color == color_of_piece)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            return check;
        }
        public Point[,,] ThreatenedPieces()
        {
            Point[,,] all_threatened_squares = new Point[2,8,8];
            return all_threatened_squares;
        }
        public bool able_to_move(Button piece)
        {
            for(int x = 0; x < this.threatened_pieces.Count; x++)
            {
                if (this.threatened_pieces[x] == piece)
                {
                    return false;
                }
            }
            return true;
        }
        public Point get_piece_square_location(Button piece)
        {
            Point piece_location = piece.Location;
            Point square_location = new Point(
                (piece_location.X + 46/2 - 70/2),
                (piece_location.Y + 46/2 - 70/2)
                );
            return square_location;
        }
        public int[] search_square_index_in_all_squares(Point square_coordinates)
        {
            int[] r = { -1, -1 };
            for(int y = 0; y < this.all_squares.GetUpperBound(0); y++)
            {
                for (int x = 0; x < this.all_squares.GetUpperBound(1); x++)
                {
                    Point point_check_square = this.all_squares[y][x].Location;
                    if(square_coordinates == point_check_square)
                    {
                        r[0] = x;
                        r[1] = y;
                        break;
                    }
                }
            }
            return r;
        }
        // pawn direction can be 1 or -1
        public bool possible_en_passant(Button pawn, int[] square)
        {
            if ( this.check_en_passant.Item1 && (pawn.Tag != this.check_en_passant.Item2.Tag & square == this.check_en_passant.Item3))
            {
                return true;
            }
            return false;
        }
        public void ChangeSquarePiece(Button new_square, Button piece, Button last_square)
        {
            this.places_pieces[last_square.TabIndex][Convert.ToInt32(last_square.Tag)] = default;
            this.places_pieces[new_square.TabIndex][Convert.ToInt32(new_square.Tag)][1] = piece;
        }
        // (Tag) for x
        // (TabIndex) for y
        // direction array size is 2 ,each element of the 2 can have just two values : (1 or 6) for item 1, and (0 or 1) for item 2 
        // for item 1 : 1 or 6 mean to the direction which is in this case the initial y square of the pawn so
        // 1 mean that the pawn have : the ability to go from the column 1 to 7 and 6 is the opposite : the ability to go from the column 6 to 0)
        // for item 2 : return 0 if the pawn is still in its initial square and 1 for the opposite(move to another square)
        public List<Point> pawn_mouvement(Button pawn, int[] direction_and_is_still_in)
        {
            // possible pawn moves :
            // take in the both sides
            // going forward
            // en passant (can be in one side but we count it as 2 because it does not matter what side where it will take a pawn (it can be do just to a pawn))
            List<Point> moves = new List<Point>();
            Point pawn_square_point = get_piece_square_location(pawn);
            int[] pawn_square_index = search_square_index_in_all_squares(pawn_square_point);
            int[][] probable_squares = {
                new int[] { pawn_square_index[1] + 1, pawn_square_index[0] },
                new int[] { pawn_square_index[1] + 2, pawn_square_index[0] },
                new int[] { pawn_square_index[1] + 1, pawn_square_index[0] + 1 },
                new int[] { pawn_square_index[1] + 1, pawn_square_index[0] - 1 }
            };
            for(int i = 0; i < probable_squares.Length; i++)
            {
                try
                {
                    Button etudied_square = this.places_pieces[probable_squares[i][1]][probable_squares[i][0]][0];
                    if(( possible_en_passant(pawn, pawn_square_index) | is_it_able(etudied_square, pawn.Tag.ToString()) ) & able_to_move(pawn))
                    {
                        moves.Add(etudied_square.Location);
                    }
                }
                catch { }
            }
            return moves;
        }
        public Point[] knight_mouvement(Button knight)
        {
            int number_of_moves = 8;
            Point[] moves = new Point[number_of_moves];
            return moves;
        }
        public List<Point> bishop_mouvement(Button bishop)
        {
            int number_of_moves = (8 * 2) - 1;
            List<Point> moves = new List<Point>();
            return moves;
        }
        public Point[] rook_mouvement(Button rook)
        {
            int number_of_moves = (8 * 2) - 1;
            Point[] moves = new Point[number_of_moves];
            return moves;
        }
        public Point[] queen_mouvement(Button queen)
        {
            int number_of_moves = (8 * 4) - 1;
            Point[] moves = new Point[number_of_moves];
            return moves;
        }
        public Tuple<bool, Point[]> castling_possible(Button king, Point[] where)
        {
            bool possibilite = true;
            Tuple<bool, Point[]> result = new Tuple<bool, Point[]>(possibilite, where);
            return result;
        }
        public Point[] king_mouvement(Button king)
        {
            int number_of_moves = 9;
            Point[] moves = new Point[number_of_moves];
            return moves;
        }
    }
}
