import { Base } from './base';

export interface Fruit extends Base {
  name:	string;
  description:	string;
  availableQuantity:	string;
  picture:	any;
  price: boolean;
}
