export interface Book {
  id?: number;
  title: string;
  author: string;
  isbn: string;
  rating?: number;
  comments?: string;
}