export interface Book {
  id?: number;
  title: string;
  author: string;
  isbn: string;
  hasNotes: boolean;
  rating?: number;
  comments?: string;
  coverImageUrl?: string;
}