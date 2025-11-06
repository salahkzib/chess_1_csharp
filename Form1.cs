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
        public static Tuple<bool, int[]> check_en_passant = new Tuple<bool, int[]>(false, default);
        public win()
        {
            InitializeComponent();
        }
        public Button[][] all_squares()
        {
            List<Button[]> arrt = new List<Button[]>()
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
            Button[][] squares = arrt.ToArray();
            return squares;
        }
        public Button[] white_pieces()
        {
            Button[] all_white_pieces = { w_pawn_a, w_pawn_b, w_pawn_c, w_pawn_d, w_pawn_e, w_pawn_f, w_pawn_g, w_pawn_h, w_queen, w_king, w_knight_1, w_knight_2, w_bishop_1, w_bishop_2, w_rook_1, w_rook_2 };
            return all_white_pieces;
        }
        public Button[] black_pieces()
        {
            Button[] all_black_pieces = { b_pawn_a, b_pawn_b, b_pawn_c, b_pawn_d, b_pawn_e, b_pawn_f, b_pawn_g, b_pawn_h, b_queen, b_king, b_knight_1, b_knight_2, b_bishop_1, b_bishop_2, b_rook_1, b_rook_2 };
            return all_black_pieces;
        }
        public Button[] all_pieces() 
        {
            Button[] all_pieces = new Button[65];
            for (int i = 0; i < 32; i++)
            {
                all_pieces[i] = white_pieces()[i];
            }
            for (int j = 32; j < 64; j++)
            {
                all_pieces[j] = black_pieces()[j];
            }
            all_pieces[65] = default;
            return all_pieces;
        }
        public Tuple<Button[][][], int[,]> places_pieces()
        {
            // squares will contain all the squares with their index
            Button[][] squares = all_squares();
            // pieces will contain all the pieces
            Button[] pieces = all_pieces();
            // len return the number of pieces
            int len = pieces.Length;
            // array will be used to know where are the pieces on the board(in which square)
            Button[][][] places_all_pieces = new Button[8][][];
            // indexs_available_squares will be used to know if a square is empty or not(0 = empty and 1 = full)
            int[,] indexs_available_squares = new int[8,8];
            // this loop will initialize indexs_available_squares with 0
            for (int h = 0; h < 8; h++)
            {
                for (int g = 0; g < 8; g++)
                {
                    indexs_available_squares[h,g] = 0;
                }
            }
            int i = 0;
            int j = 0;
            int k = 0;
            // this algorithm will return a 3D array which is places_all_pieces with the pieces
            // and their squares and 2D array indexs_available_squares with 0(available square) and 1 (unavailable square)
            while (i < 8)
            {
                j = 0;
                while(j < 8)
                {
                    k = 0;
                    while (squares[i][j].Location != get_piece_square_location(pieces[k]) & k <= 64)
                    {
                        k++;
                    }
                    places_all_pieces[i][j] = new Button[2] { pieces[k], squares[i][j] };
                    if (k != 65)
                    {
                        indexs_available_squares[i,j] = 1;
                    }
                    j++;
                }
                i++;
            }
            // craeting a tuple to return both arrays
            Tuple<Button[][][], int[,]> result = new Tuple<Button[][][], int[,]>(places_all_pieces, indexs_available_squares);
            return result;
        }
        public Boolean is_it_able(Button square, string color_of_piece)
        {
            // x_square is the row of the square (Tag)
            int x_square = Convert.ToInt32(square.Tag);
            // y_square is the column of the square (TabIndex)
            int y_square = square.TabIndex;
            // check_number will be 0 if the square is empty and 1 if the square is full
            int check_number = places_pieces().Item2[y_square, x_square];
            bool check = default;
            // checking if the square is empty or full
            if (check_number == 0)
            {
                check = true;
            }
            else if (check_number == 1)
            {
                string check_color = places_pieces().Item1[y_square][x_square][0].Tag.ToString();
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
        public Point[,,] threatened_squares()
        {
            Point[,,] all_threatened_squares = new Point[2,8,8];
            return all_threatened_squares;
        }
        public Boolean able_to_move()
        {
            bool is_it = true;
            return is_it;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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
            for(int y = 0; y < all_squares().GetUpperBound(0); y++)
            {
                for (int x = 0; x < all_squares().GetUpperBound(1); x++)
                {
                    Point point_check_square = all_squares()[y][x].Location;
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
        public object[] possible_en_passant(Button pawn, int pawn_direction)
        {
            Point taken_square = default;
            bool check_possibility = false;
            Point pawn_position = get_piece_square_location(pawn);
            int[] index_of_square = search_square_index_in_all_squares(pawn_position);
            if (check_en_passant.Item1 == true && check_en_passant.Item2[1] == index_of_square[1])
            {
                taken_square = new Point(check_en_passant.Item2[0], index_of_square[1] + pawn_direction);
                check_possibility = true;
            }
            object[] result = {check_possibility, taken_square};
            return result;
        }
        // (Tag) for x
        // (TabIndex) for y
        // direction array size is 2 ,each element of the 2 can have just two values : (1 or 6) for item 1, and (0 or 1) for item 2 
        // for item 1 : 1 or 6 mean to the direction which is in this case the initial y square of the pawn so
        // 1 mean that the pawn have : the ability to go from the column 1 to 7 and 6 is the opposite : the ability to go from the column 6 to 0)
        // for item 2 : return 0 if the pawn is still in its initial square and 1 for the opposite(move to another square)
        public Point[] pawn_mouvement(Button pawn, int[] direction_and_is_still_in)
        {
            // possible pawn moves :
            // take in the both sides
            // going forward
            // en passant (can be in one side but we count it as 2 because it does not matter what side where it will take a pawn (it can be do just to a pawn))
            bool can_move = able_to_move();
            int number_of_moves = 4;
            Point[] moves = new Point[number_of_moves];
            Point pawn_square = get_piece_square_location(pawn);
            int[] pawn_index = search_square_index_in_all_squares(pawn_square);
            Point[] probable_squares = { 
                all_squares()[pawn_index[1]][pawn_index[0]].Location,
                all_squares()[pawn_index[1]][pawn_index[0]].Location,
                all_squares()[pawn_index[1]][pawn_index[0]].Location,
                all_squares()[pawn_index[1]][pawn_index[0]].Location
            };
            int[] cases = new int[number_of_moves];
            if (able_to_move() & is_it_able(pawn, pawn.Tag.ToString()) & direction_and_is_still_in[1] == 0)
            {
            }
            int case_element_index;
            int moves_index = 0;
            for(int index = 0; index < number_of_moves; index++)
            {
                case_element_index = cases[index];
                switch (case_element_index)
                {
                    case 0:
                        break;
                    case 1:
                        moves[moves_index] = probable_squares[index];
                        moves_index++;
                        break;
                }
            }
            return moves;
        }
        public Point[] knight_mouvement(Button knight)
        {
            int number_of_moves = 8;
            Point[] moves = new Point[number_of_moves];
            return moves;
        }
        public Point[] bishop_mouvement(Button bishop)
        {
            int number_of_moves = (8 * 2) - 1;
            Point[] moves = new Point[number_of_moves];
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
        private void w_pawn_a_Click(object sender, EventArgs e)
        {
            

        }
        private void b16_Click(object sender, EventArgs e)
        {

        }

        private void b37_Click(object sender, EventArgs e)
        {

        }

        private void b33_Click(object sender, EventArgs e)
        {

        }
    }
}
