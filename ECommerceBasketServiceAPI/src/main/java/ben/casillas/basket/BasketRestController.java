package ben.casillas.basket;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

//import java.time.LocalDate;

@RestController
@RequestMapping("/basket")
public class BasketRestController {
    @Autowired
    private RedisTemplate<String, Basket> redisTemplate;

    @PostMapping(path = "/{basketID}") //add to a basket, either a new or existing one
    @ResponseStatus(code = HttpStatus.CREATED)
    public Basket addSingleItemToBasket(@PathVariable String basketID, @RequestBody Item item){
        Basket basket = null;
        if("new".equals(basketID)){
           basket = new Basket(UUID.randomUUID().toString()); //make a new basket if new is passed into path
        } else {
            basket = redisTemplate.opsForValue().get(basketID);
        }
        basket.getItems().add(item); //book in request body, add that boi to basket
        redisTemplate.opsForValue().set(basket.getBasketGuid(), basket);
        return basket;
    }

    @PostMapping(path = "/addBooks/{basketID}") //add to a basket, either a new or existing one
    @ResponseStatus(code = HttpStatus.CREATED)
    public Basket addItemsToBasket(@PathVariable String basketID, @RequestBody List<Item> items){
        Basket basket = null;
        if("new".equals(basketID)){
            basket = new Basket(UUID.randomUUID().toString()); //make a new basket if new is passed into path
        } else {
            basket = redisTemplate.opsForValue().get(basketID);
        }
        for (Item item : items){
            basket.getItems().add(item);
        }
        //basket.getBooks().addAll(books);
        redisTemplate.opsForValue().set(basket.getBasketGuid(), basket);
        return basket;
    }

    @GetMapping(path = "/{basketID}")
    @ResponseStatus(code = HttpStatus.OK)
    public Basket GetBasket(@PathVariable  String basketID){
        Basket basket = redisTemplate.opsForValue().get(basketID);
        return basket;
    }

//    public List<Basket> GetAllBaskets(){
//        redisTemplate.
//    }

    @DeleteMapping(path = "/{basketID}/{itemUUID}")
    @ResponseStatus(code = HttpStatus.OK)
    public void DeleteItem(@PathVariable String basketID, @PathVariable UUID itemUUID){ //make sure to validate if the book exists in their basket
        Basket basket = redisTemplate.opsForValue().get(basketID);
        basket.setItems(basket.getItems().stream().filter(b -> !b.getItemUUID().equals(itemUUID)).collect(Collectors.toList()));
        redisTemplate.opsForValue().set(basketID, basket);
    }
    @DeleteMapping(path = "/{basketID}")
    @ResponseStatus(HttpStatus.OK)
    public void DeleteBasket(@PathVariable String basketID){
        redisTemplate.delete(basketID);
    }

}