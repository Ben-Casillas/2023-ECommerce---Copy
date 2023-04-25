package ben.casillas.itemService;

import java.util.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/item")
public class ItemRestController {
    @Autowired
    private ItemRepository itemsRepo;

    //POST working in postman
    @PostMapping(path = "/")
    @ResponseStatus(code = HttpStatus.CREATED)
    public void createItem(@RequestBody Item item) { //get the request body which will be type Item
        addItemToDB(item, itemsRepo);
    }

    //POST test
    @PostMapping(path ="addItems" )
    @ResponseStatus(HttpStatus.CREATED)
    public void createItems(@RequestBody List<Item> items){
        for(Item item : items) {
            addItemToDB(item, itemsRepo);
        }
    }

    //GET working in postman
    @GetMapping(path = "")
    @ResponseStatus(HttpStatus.OK)
    public List<Item> findAllItems() {
        return itemsRepo.findAll();
    }

    //GET working in postman
    @GetMapping(path = "/{uuid}")
    @ResponseStatus(HttpStatus.OK)
    public Item findItemById(@PathVariable(required = true) UUID uuid) {
        return itemsRepo.findById(uuid).orElseThrow(() -> new NoSuchElementException());
    }

    //GET working in postman
    @GetMapping(path ="/search/{searchText}")
    @ResponseStatus(HttpStatus.OK)
    public List<Item> searchItems(@PathVariable (required = true) String searchText){
        return itemsRepo.findByTitleContainingOrDescriptionContaining(searchText, searchText);
    }

    //PUT
    @PutMapping(path = "/{uuid}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void updateItem(@PathVariable (required = true) UUID uuid, @RequestBody Item updatedItem){
        if(!updatedItem.getItemUUID().equals(uuid)){ //if the items
            throw new RuntimeException("The two values did not match"); //if no item with that id exists
        } else {
            itemsRepo.save(updatedItem);
        }
    }

    //DELETE
    @DeleteMapping(path = "{uuid}")
    public void deleteItem(@PathVariable UUID uuid){
        itemsRepo.deleteById(uuid);
    }


    //helper functions
    private static void addItemToDB(Item item, ItemRepository itemsRepo){
        item.setItemUUID(UUID.randomUUID()); //set uuid here with random generated one
        itemsRepo.save(item);
    }
}
